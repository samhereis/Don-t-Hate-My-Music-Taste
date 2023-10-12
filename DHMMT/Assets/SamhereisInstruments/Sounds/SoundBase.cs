using Helpers;
using System;
using UnityEngine;

namespace Sound
{
    [Serializable]
    public class SoundBase
    {
        public bool hasAudio => _audioClips.Length > 0;
        internal AudioClip audioClip { get { if (_audioClips.Length > 0) return _audioClips.GetRandom(); else return null; } }

        [SerializeField] private AudioClip[] _audioClips;

        [field: SerializeField] public bool isMain { get; private set; } = false;
        [field: SerializeField] public bool loop { get; private set; } = false;
        [field: SerializeField] public bool disableOthers { get; private set; } = false;
        [field: SerializeField] public float volume { get; private set; } = 1;
        [field: SerializeField] public float distance { get; private set; } = 50;
    }
}