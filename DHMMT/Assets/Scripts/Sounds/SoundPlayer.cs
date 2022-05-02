using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

namespace Sripts
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class SoundPlayer : MonoBehaviour
    {
        public static SoundPlayer instance { get; private set; }

        [Header("Componenets")]
        [SerializeField] private AudioSource _mainAudioSource;
        [SerializeField] private List<AudioSource> _audioSourcePool;
        [SerializeField] private AudioMixer _mixer;

        [Header("Settings")]
        [SerializeField] private int _auioSourcePoolCount = 0;
        [SerializeField] private bool _isUI;

        private void Awake()
        {
            if(instance != this) instance = this;
        }

        private void OnDestroy()
        {
            if (instance == this) instance = null;
        }

        private void OnValidate()
        {
            Setup();
        }

        public async void Play(AudioClip audioClip, bool disableAll = false, float volume = 0.7f, float distance = 0)
        {
            if (disableAll)
            {
                foreach (AudioSource audioSource in _audioSourcePool)
                {
                    await AsyncHelper.Delay();

                    audioSource.Stop();
                }

                _mainAudioSource.clip = audioClip;
                _mainAudioSource.volume = volume;

                if(distance > 0)
                {
                    _mainAudioSource.maxDistance = distance * 2;
                    _mainAudioSource.minDistance = 0;
                }

                _mainAudioSource.Play();
            }
            else
            {
                var freeAudioSurce = _audioSourcePool.Find(x => x.isPlaying == false);

                if (freeAudioSurce == null) freeAudioSurce = _audioSourcePool[0];

                freeAudioSurce.Stop();
                freeAudioSurce.clip = audioClip;
                freeAudioSurce.volume = volume;

                if (distance > 0)
                {
                    freeAudioSurce.maxDistance = distance * 2;
                    freeAudioSurce.minDistance = 0;
                }

                freeAudioSurce.Play();
            }
        }

        public bool TryPlay(SimpleAudio sound)
        {
            if (sound != null && sound.hasAudio)
            {
                Play(sound.audioClip, sound.disableAll, sound.volume, sound.distance);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TryPlay(EventBasedAudio sound)
        {
            if (sound != null && sound.hasAudio)
            {
                Play(sound.audioClip, sound.disableAll, sound.volume, sound.distance);
                return true;
            }
            else
            {
                return false;
            }
        }

        [ContextMenu("Setup")] public void Setup()
        {
            if (_mainAudioSource == null) _mainAudioSource = GetComponent<AudioSource>();

            _audioSourcePool = GetComponentsInChildren<AudioSource>().ToList();

            _audioSourcePool.ForEach(x =>
            {
                x.playOnAwake = false;
                x.spatialBlend = 1;
                x.rolloffMode = AudioRolloffMode.Linear;
            } );

            _audioSourcePool.RemoveAll(x => x == null || x.gameObject == gameObject);

            if(_audioSourcePool.Count != _auioSourcePoolCount)
            {
                foreach (AudioSource audioSource in _audioSourcePool)
                {
                    DestroyImmediate(audioSource.gameObject);
                }
                _audioSourcePool.Clear();

                for (int i = 0; i < _auioSourcePoolCount; i++)
                {
                    var obj = new GameObject("A sound");
                    obj.transform.parent = transform;
                    obj.AddComponent<AudioSource>();

                    _audioSourcePool.Add(obj.GetComponent<AudioSource>());
                }
            }

            if(_mixer == null)
            {
                AddressablesHelper.LoadAndDo<AudioMixer>("AudioMixer", (x) =>
                {
                    _mixer = x;
                    _mainAudioSource.outputAudioMixerGroup = _mixer.FindMatchingGroups("Effect")[0];
                    _audioSourcePool.ForEach(x =>
                    {
                        x.outputAudioMixerGroup = _mixer.FindMatchingGroups("Effect")[0];
                    });
                });
            }
            else
            {
                try
                {
                    if(_mixer.FindMatchingGroups("Effect").Count() > 0)
                    {
                        _mainAudioSource.outputAudioMixerGroup = _mixer.FindMatchingGroups("Effect")[0];
                        _audioSourcePool.ForEach(x => { x.outputAudioMixerGroup = _mixer.FindMatchingGroups("Effect")[0]; });
                    }
                }
                catch (Exception ex) { Debug.LogWarning(ex); }
            }
        }
    }



    [Serializable] public class EventBasedAudio
    {
        public EventBasedAudio(string name)
        {
            eventName = name;
        }

        [SerializeField] internal string eventName;

        [SerializeField] internal AudioClip audioClip
        {
            get
            {
                if (_audioClips.Length > 0) return _audioClips[UnityEngine.Random.Range(0, _audioClips.Length)];
                else return null;
            }
        }

        [SerializeField] internal AudioClip[] _audioClips;
        [SerializeField] internal bool disableAll;
        [SerializeField] internal float volume = 1;
        [SerializeField] internal float distance = 50;

        public bool hasAudio => _audioClips.Length > 0;
    }

    [Serializable] public class SimpleAudio
    {
        internal AudioClip audioClip
        {
            get
            {
                if (_audioClips.Length > 0) return _audioClips[UnityEngine.Random.Range(0, _audioClips.Length)];
                else return null;
            }
        }

        [Header("Will play random one from the list")]
        [SerializeField] internal AudioClip[] _audioClips;
        [SerializeField] internal bool disableAll;
        [SerializeField] internal float volume = 1;
        [SerializeField] internal float distance = 50;

        public bool hasAudio => _audioClips.Length > 0;
    }
}