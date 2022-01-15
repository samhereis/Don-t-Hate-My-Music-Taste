using System;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoundMusicManager_MainMenu : MonoBehaviour
{
    // Check if player has music on computer

    public string musicFolderPath => MusicList_SO.MusicFolderPath;

    private int _musicCount;
    public int musicCount => _musicCount;

    [SerializeField] private GameObject _found;
    [SerializeField] private GameObject _notFound;

    [SerializeField] private MusicList_SO _musicList;

    //[SerializeField] private UnityEngine.Localization.Components.LocalizeStringEvent _foundText;

    [SerializeField] AudioSource _audioSource;

    private void Awake()
    {
        if (Directory.Exists(musicFolderPath) == false)
        {
            Directory.CreateDirectory(musicFolderPath);
        }

        _audioSource.Stop();
    }

    private void FixedUpdate()
    {
        if (_audioSource.isPlaying == false && _musicList.MusicList.Count > 0)
        {
            _audioSource.clip = _musicList.MusicList[Random.Range(0, _musicList.MusicList.Count)];

            _audioSource.Play();
        }

        //musicCount = _musicList.MusicList.Count;

        //_foundText.RefreshString();
    }
}
