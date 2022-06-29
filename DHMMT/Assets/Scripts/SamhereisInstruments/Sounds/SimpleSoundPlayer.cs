using Sripts;
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
