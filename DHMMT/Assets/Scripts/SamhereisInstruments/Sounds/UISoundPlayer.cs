using Samhereis.Helpers;
using System.Collections.Generic;
using UnityEngine;

namespace Samhereis.Sound
{
    public class UISoundPlayer : MonoBehaviour
    {
        [SerializeField] private List<EventBasedAudio> _sounds = new List<EventBasedAudio>();

        private async void OnValidate()
        {
            foreach (var sound in _sounds)
            {
                await AsyncHelper.Delay(() =>
                {
                    sound.volume = 1;
                    sound.distance = 1000;
                });
            }
        }

        public void Play(string eventName)
        {
            var sound = _sounds.Find(x => x.eventName == eventName);

            SoundPlayer.instance?.TryPlay(sound);
        }
    }
}