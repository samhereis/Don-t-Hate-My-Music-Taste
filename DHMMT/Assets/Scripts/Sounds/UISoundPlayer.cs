using Sripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SoundPlayers
{
    public class UISoundPlayer : MonoBehaviour
    {
        [SerializeField] private List<EventBasedAudio> _sounds = new List<EventBasedAudio>();

        private void OnValidate()
        {
            foreach (var sound in _sounds)
            {
                sound.volume = 1;
                sound.distance = 1000;
            }
        }

        public void Play(string eventName)
        {
            var sound = _sounds.Find(x => x.eventName == eventName);

            SoundPlayer.instance?.TryPlay(sound);
        }
    }
}