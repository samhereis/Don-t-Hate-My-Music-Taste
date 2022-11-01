using Helpers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sound
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class SoundPlayer : MonoBehaviour
    {
        public static SoundPlayer instance { get; private set; }

        public AudioClip currentMainAudioCLip => _mainAudioSource.clip;

        [Header("Componenets")]
        [SerializeField] private AudioSource _mainAudioSource;
        [SerializeField] private List<AudioSource> _audioSourcePool;

        [Header("Settings")]
        [SerializeField] private int _auioSourcePoolCount = 0;
        [SerializeField] private bool _isGlobal;

        private void Awake()
        {
            if (instance == null && _isGlobal == true) instance = this;
        }

        private void OnDestroy()
        {
            if (instance == this && _isGlobal == true) instance = null;
        }

        private void OnValidate()
        {
            Setup();
        }

        public async void TryPlay(SoundBase sound)
        {
            if (sound.audioClip == null) return;

            if (sound.isMain)
            {
                if (_mainAudioSource.clip == sound.audioClip) return;

                _mainAudioSource.Stop();
                _mainAudioSource.clip = sound.audioClip;
                _mainAudioSource.volume = sound.volume;

                if (sound.distance > 0)
                {
                    _mainAudioSource.maxDistance = sound.distance * 2;
                    _mainAudioSource.minDistance = 0;
                }

                _mainAudioSource.loop = sound.loop;
                _mainAudioSource.Play();

                if (sound.disableOthers) foreach (AudioSource audioSource in _audioSourcePool) await AsyncHelper.Delay(() => audioSource.Stop());
            }
            else
            {
                var freeAudioSurce = _audioSourcePool.Find(x => x.isPlaying == false);

                if (freeAudioSurce == null) freeAudioSurce = _audioSourcePool[0];

                freeAudioSurce.Stop();
                freeAudioSurce.clip = sound.audioClip;
                freeAudioSurce.volume = sound.volume;

                if (sound.distance > 0)
                {
                    freeAudioSurce.maxDistance = sound.distance * 2;
                    freeAudioSurce.minDistance = 0;
                }

                freeAudioSurce.loop = sound.loop;
                freeAudioSurce.Play();
            }
        }

        [ContextMenu("Setup")]
        public void Setup()
        {
            if (_mainAudioSource == null) _mainAudioSource = GetComponent<AudioSource>();

            _audioSourcePool = GetComponentsInChildren<AudioSource>().ToList();

            _audioSourcePool.RemoveAll(x => x == null || x.gameObject == gameObject);

            if (_audioSourcePool.Count != _auioSourcePoolCount)
            {
                foreach (AudioSource audioSource in _audioSourcePool) DestroyImmediate(audioSource.gameObject);
                _audioSourcePool.Clear();

                for (int i = 0; i < _auioSourcePoolCount; i++)
                {
                    var obj = new GameObject("A sound");
                    obj.transform.parent = transform;
                    obj.AddComponent<AudioSource>();

                    _audioSourcePool.Add(obj.GetComponent<AudioSource>());
                }
            }
        }
    }
}