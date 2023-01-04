using Helpers;
using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    public class UISoundPlayer : MonoBehaviour
    {
        [SerializeField] private List<EventBasedSound> _sounds = new List<EventBasedSound>();

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