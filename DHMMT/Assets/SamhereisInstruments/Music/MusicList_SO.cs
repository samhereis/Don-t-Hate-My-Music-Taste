using Helpers;
using Interfaces;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Music
{
    [CreateAssetMenu(fileName = "MusicList", menuName = "Scriptables/MusicList")]
    public class MusicList_SO : ScriptableObject, IInitializable
    {
        public static string MusicFolderPath => $"{System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyMusic)}/DHMMT";

        [field: SerializeField] public List<AudioClip> musicList { get; private set; } = new List<AudioClip>();

        [SerializeField] private List<AudioClip> _defaultMusicList;

        public bool hasMusic => count > 0;
        public int count => musicList.Count;

        public async void Initialize()
        {
            Clear();

            if (Directory.Exists(MusicFolderPath) == false) Directory.CreateDirectory(MusicFolderPath);

            var files = Directory.GetFiles(MusicFolderPath);

            if (files.Length == 0)
            {
                foreach (var item in _defaultMusicList)
                {
                    musicList.SafeAdd(item);
                }
            }
            else
            {
                foreach (string file in files)
                {
                    if (File.Exists(file))
                    {
                        using (var uwr = UnityWebRequestMultimedia.GetAudioClip("file://" + file, AudioType.MPEG))
                        {
                            ((DownloadHandlerAudioClip)uwr.downloadHandler).streamAudio = true;

                            var wait = uwr.SendWebRequest();

                            while (wait.isDone == false) { await AsyncHelper.Delay(); }

                            DownloadHandlerAudioClip dlHandler = (DownloadHandlerAudioClip)uwr.downloadHandler;

                            if (dlHandler.isDone) if (dlHandler.audioClip != null) musicList.SafeAdd(dlHandler.audioClip);
                        }
                    }
                }
            }
        }

        private void Clear()
        {
            musicList.Clear();
        }
    }
}