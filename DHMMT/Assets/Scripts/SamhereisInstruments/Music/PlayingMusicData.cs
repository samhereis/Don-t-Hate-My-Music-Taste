using Helpers;
using System.Threading;
using UnityEngine;

namespace Music
{
    public class PlayingMusicData : MonoBehaviour
    {
        [SerializeField] private SpectrumData _spectrumData;
        [SerializeField] private MusicList_SO _musicList;

        [Header("Settings")]
        [SerializeField] private bool _shouldSearch = true;
        [SerializeField] private bool _autoPlay = false;

        [Header("Components")]
        [SerializeField] private AudioSource _audioSource;

        [Header("Debug")]
        [SerializeField] private bool _isCheckingForAudio = false;

        private CancellationTokenSource _cancellationTokenSource;

        private void Awake()
        {
            if (_audioSource == null) _audioSource = GetComponent<AudioSource>();
        }

        private async void OnEnable()
        {
            if (_shouldSearch == true) _musicList.LoadMusic();
            if (_autoPlay == true) await AsyncHelper.DelayAndDo(2000, () => CheckForAudio(_cancellationTokenSource = new CancellationTokenSource()));

            //PlayAudio();
        }

        private void Update()
        {
            _spectrumData.SetSpectrumWidth(_audioSource);
        }

        private void OnDisable()
        {
            _cancellationTokenSource.Cancel();
        }

        public void PauseMusic(bool pause)
        {
            if (pause) _audioSource.Pause(); else _audioSource.UnPause();
        }

        private async void CheckForAudio(CancellationTokenSource cancellationTokenSource)
        {
            if (_isCheckingForAudio) return;
            _isCheckingForAudio = true;

            while (cancellationTokenSource.IsCancellationRequested == false)
            {
                if (_audioSource.isPlaying == false && _musicList.count > 0)
                {
                    PlayAudio();
                }

                await AsyncHelper.Delay(1);
            }

            _isCheckingForAudio = false;
        }

        private void PlayAudio()
        {
            _audioSource.clip = null;
            _audioSource.clip = _musicList.musicList[Random.Range(0, _musicList.count)];
            _audioSource.Play();
        }
    }
}