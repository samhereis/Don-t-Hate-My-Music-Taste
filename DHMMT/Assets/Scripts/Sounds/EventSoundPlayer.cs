using Sripts;
using System.Collections.Generic;
using UnityEngine;

namespace SoundPlayers
{
    public class EventSoundPlayer : MonoBehaviour
    {
        [SerializeField] private SoundPlayer _soundPlayer;
        [SerializeField] private List<EventBasedAudio> _sounds = new List<EventBasedAudio>();

        private void OnValidate()
        {
            if (_soundPlayer == null) _soundPlayer = GetComponentInChildren<SoundPlayer>(true);

            foreach (var sound in _sounds)
            {
                sound.volume = 1;
                if (sound.distance == 0) sound.distance = 10;
            }
        }

        private void Awake()
        {
            if (_soundPlayer == null) _soundPlayer = GetComponentInChildren<SoundPlayer>(true);
        }

        public void Play(string eventName)
        {
            var sound = _sounds.Find(x => x.eventName == eventName);

            _soundPlayer?.TryPlay(sound);
        }
    }
}