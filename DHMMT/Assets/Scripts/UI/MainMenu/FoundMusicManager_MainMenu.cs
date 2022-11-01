using Music;
using System.IO;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace UI
{
    public class FoundMusicManager_MainMenu : MonoBehaviour
    {
        public string musicFolderPath => MusicList_SO.MusicFolderPath;
        public int musicCount => _musicList.count;

        [SerializeField] private GameObject _found, _notFound;
        [SerializeField] private MusicList_SO _musicList;
        [SerializeField] private LocalizeStringEvent _musicCountString;

        private void Awake()
        {
            if (Directory.Exists(musicFolderPath) == false) Directory.CreateDirectory(musicFolderPath);
        }

        private void FixedUpdate()
        {
            _musicCountString.RefreshString();

            _found.SetActive(_musicList._hasMusic);
            _notFound.SetActive(!_musicList._hasMusic);
        }
    }
}