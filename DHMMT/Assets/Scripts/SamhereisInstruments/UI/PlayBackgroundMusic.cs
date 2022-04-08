using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using UnityEngine.Events;
using Helpers;

namespace Sripts
{
    public sealed class PlayBackgroundMusic : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField][Range(0, 5000)] private int _delayInMiliseconds; 

        private async void OnEnable()
        {
            await AsyncHelper.Delay(_delayInMiliseconds);

            PlayAudio();
        }

        private void PlayAudio()
        {
            _audioSource.Play();
        }
    }
}