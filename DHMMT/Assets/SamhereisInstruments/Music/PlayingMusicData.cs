using ConstStrings;
using DI;
using Helpers;
using UnityEngine;

namespace Music
{
    public class PlayingMusicData : MonoBehaviour, IDIDependent
    {
        [DI(DIStrings.spectrumDataContainer)][SerializeField] private SpectrumData _spectrumData;
        [DI(DIStrings.musicList)][SerializeField] private MusicList_SO _musicList;

        [Header("Components")]
        [SerializeField] private AudioSource _audioSource;

        [Header("Debug")]
        [SerializeField] private bool _isCheckingForAudio = false;
        [SerializeField] private bool _isActive = false;

        private void Start()
        {
            (this as IDIDependent).LoadDependencies();

            if (_audioSource == null) _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (_spectrumData != null)
            {
                _spectrumData.SetSpectrumWidth(_audioSource);
            }
        }

        public void SetActive(bool targetActiveStatus, bool controlPlay = false)
        {
            _isActive = targetActiveStatus;

            if (controlPlay)
            {
                if (_isActive == true)
                {
                    CheckForAudio();
                }
                else
                {
                    _audioSource.Stop();
                }
            }
        }

        public void PauseMusic(bool pause)
        {
            if (pause == true)
            {
                SetActive(false);
                _audioSource.Pause();
            }
            else
            {
                _audioSource.UnPause();
                SetActive(true);
            }
        }

        private async void CheckForAudio()
        {
            if (_isCheckingForAudio) return;
            _isCheckingForAudio = true;

            while (destroyCancellationToken.IsCancellationRequested == false)
            {
                if (_isActive == true)
                {
                    TryPlayMusicIfNotPlaying();
                }

                await AsyncHelper.DelayInt(1);
            }

            _isCheckingForAudio = false;
        }

        private void TryPlayMusicIfNotPlaying()
        {
            if ((_audioSource.isPlaying == false || _audioSource.clip == null) && _musicList.count > 0)
            {
                _audioSource.clip = null;
                _audioSource.clip = _musicList.musicList[Random.Range(0, _musicList.count)];
                _audioSource.Play();
            }
        }
    }
}