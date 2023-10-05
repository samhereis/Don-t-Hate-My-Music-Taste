using DI;
using Helpers;
using ConstStrings;
using System.Threading;
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

        private CancellationTokenSource _onDestroyCT = new CancellationTokenSource();

        private void Awake()
        {
            (this as IDIDependent).LoadDependencies();

            if (_audioSource == null) _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            CheckForAudio(_onDestroyCT);
        }

        private void Update()
        {
            _spectrumData.SetSpectrumWidth(_audioSource);
        }

        private void OnDestroy()
        {
            _onDestroyCT.Cancel();
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
                if ((_audioSource.isPlaying == false || _audioSource.clip == null) && _musicList.count > 0)
                {
                    _audioSource.clip = null;
                    _audioSource.clip = _musicList.musicList[Random.Range(0, _musicList.count)];
                    _audioSource.Play();
                }

                await AsyncHelper.Delay(1);
            }

            _isCheckingForAudio = false;
        }
    }
}