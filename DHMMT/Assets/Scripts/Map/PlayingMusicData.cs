using Scriptables.Holders.Music;
using UnityEngine;

public class PlayingMusicData : MonoBehaviour
{
    [SerializeField] private SpectrumData _spectrumData;
    [SerializeField] private MusicList_SO _musicList;

    [Header("Search for music data")]
    [SerializeField] private bool _shouldSearch;

    [Header("Components")]
    [SerializeField] private AudioSource _audioSource;

    private void Awake()
    {
        if (_audioSource == null) _audioSource = GetComponent<AudioSource>();

        if (_shouldSearch)
        {
            _musicList.LoadMusic();
        }
    }

    private void Update()
    {
        _spectrumData.SetSpectrumWidth(_audioSource);
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

    private void checkForAudio()    /* Check if any audio is playing */
    {
        if (!_audioSource.isPlaying && _musicList.musicList.Count > 0)
        {
            _audioSource.clip = _musicList.musicList[Random.Range(0, _musicList.musicList.Count)];

            _audioSource.Play();
        }
    }
}

