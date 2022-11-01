using DG.Tweening;
using UnityEngine;

namespace Sound
{
    public sealed class BackgroundMusicPlayer : MonoBehaviour
    {
        public static BackgroundMusicPlayer instance;

        [Header("Settings")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private float _transitionDuration = 1;

        private void Awake()
        {
            instance = this;
        }

        private void OnDestroy()
        {
            instance = null;
        }

        public void PlayMusic(SoundBase sound)
        {
            _audioSource.clip = sound.audioClip;
            _audioSource.loop = sound.loop;

            PlayMusic();

            _audioSource.Play();
        }

        private void PlayMusic()
        {
            _audioSource.DOKill();
            _audioSource.DOFade(1, _transitionDuration);
        }

        public void StopMusic()
        {
            _audioSource.DOKill();
            _audioSource.DOFade(0, _transitionDuration);
        }
    }
}