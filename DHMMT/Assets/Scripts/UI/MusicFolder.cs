using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MusicFolder : MonoBehaviour
{
    // Check if player has music on computer

    public string MusicFolderPath { get => $"{System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyMusic)}/DHMMT"; }
    public int MusicCount;

    //[SerializeField] private GameObject _found;
    //[SerializeField] private GameObject _notFound;

    [SerializeField] ScriptableMusicList _musicList;

    [SerializeField] private UnityEngine.Localization.Components.LocalizeStringEvent foundText;

    [SerializeField] AudioSource _audioSource;

    private void Awake()
    {
        if (System.IO.Directory.Exists(MusicFolderPath) == false)
        {
            System.IO.Directory.CreateDirectory(MusicFolderPath);
        }
    }

    private void FixedUpdate()
    {
        if (_audioSource.isPlaying == false && _musicList.MusicList.Count > 0)
        {
            _audioSource.clip = _musicList.MusicList[Random.Range(0, _musicList.MusicList.Count)];

            _audioSource.Play();
        }

        MusicCount = _musicList.MusicList.Count;

        foundText.RefreshString();
    }
}
