using Sripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundPlayers
{
    public class SimpleSoundPlayer : MonoBehaviour
    {
        [SerializeField] private SoundPlayer _soundPlayer;
        [SerializeField] private SimpleAudio _soundSettings;

        public void Play()
        {
            _soundPlayer?.TryPlay(_soundSettings);
        }
    }
}
