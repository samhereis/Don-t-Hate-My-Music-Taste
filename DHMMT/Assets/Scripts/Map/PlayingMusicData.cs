using Scriptables.Holders.Music;
using System.Threading;
using UnityEngine;
using Helpers;

public class PlayingMusicData : MonoBehaviour
{
    [SerializeField] private SpectrumData _spectrumData;
    [SerializeField] private MusicList_SO _musicList;

    [Header("Search for music data")]
    [SerializeField] private bool _shouldSearch;

    [Header("Components")]
    [SerializeField] private AudioSource _audioSource;

    private CancellationTokenSource _cancellationTokenSource;

    private void Awake()
    {
        _audioSource ??= GetComponent<AudioSource>();
    }

    private async void OnEnable()
    {
        await AsyncHelper.Delay(2);

        CheckForAudio(_cancellationTokenSource = new CancellationTokenSource());
    }

    private void Start()
    {
        if (_shouldSearch) _musicList.LoadMusic();
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
        if (pause)
        {
            _audioSource.Pause();
        }
        else
        {
            _audioSource.UnPause();
        }
    }

    private bool _isCheckingForAudio = false;
    private async void CheckForAudio(CancellationTokenSource cancellationTokenSource)
    {
        if (_isCheckingForAudio) return;

        _isCheckingForAudio = true;

        while (!cancellationTokenSource.IsCancellationRequested)
        {
            if ((!_audioSource.isPlaying || _audioSource.clip == null) && _musicList.count > 0)
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

