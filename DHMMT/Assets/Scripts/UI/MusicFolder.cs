using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MusicFolder : MonoBehaviour
{
    // Check if player has music on computer

    public string MusicFolderPath { get => $"{System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyMusic)}/DHMMT"; }
    public int MusicCount;

    [SerializeField] private GameObject _found;
    [SerializeField] private GameObject _notFound;

    [SerializeField] ScriptableMusicList _musicList;

    [SerializeField] private UnityEngine.Localization.Components.LocalizeStringEvent foundText;

    private void Awake()
    {
        if (System.IO.Directory.Exists(MusicFolderPath) == false)
        {
            System.IO.Directory.CreateDirectory(MusicFolderPath);
        }

        StartCoroutine(_musicList.loadMusic());
    }

    private void FixedUpdate()
    {
        if (_musicList.MusicList.Count == 0)
        {
            _found.SetActive(false);
            _notFound.SetActive(true);
        }
        else if (_musicList.MusicList.Count > 0)
        {
            MusicCount = _musicList.MusicList.Count;

            _notFound.SetActive(false);
            _found.SetActive(true);
        }

        foundText.RefreshString();
    }
}
