using Samhereis.Helpers;
using System;
using UnityEngine;

namespace Samhereis.Music
{
    public sealed class PlayBackgroundMusic : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField][Range(0, 5000)] private int _delayInMiliseconds;

        private async void OnEnable()
        {
            await AsyncHelper.DelayAndDo(_delayInMiliseconds, () => PlayAudio());
        }

        private void PlayAudio()
        {
            _audioSource.Play();
        }
    }
}