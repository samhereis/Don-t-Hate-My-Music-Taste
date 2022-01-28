using Helpers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "MusicList", menuName = "Scriptables/MusicList")]
public class MusicList_SO : ScriptableObject
{
    // Default music to play when player doesn't have music on computer

    public static string MusicFolderPath => $"{System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyMusic)}/DHMMT";
    public bool _hasMusic => _musicList.Count > 0;

    [SerializeField] private List<AudioClip> _musicList = new List<AudioClip>();
    public List<AudioClip> musicList { get { if (_musicList.Count > 0) return _musicList; else return _defaultMusicList; } }

    [SerializeField] private List<AudioClip> _defaultMusicList;

    public int count => _musicList.Count;

    public async void LoadMusic()
    {
        Clear();

        foreach (string file in System.IO.Directory.GetFiles(MusicFolderPath))
        {
            if (System.IO.File.Exists(file))
            {
                using (var uwr = UnityWebRequestMultimedia.GetAudioClip("file://" + file, AudioType.MPEG))
                {
                    ((DownloadHandlerAudioClip)uwr.downloadHandler).streamAudio = true;

                    var wait = uwr.SendWebRequest();

                    while (!wait.isDone) { await AsyncHelper.Delay(); }

                    DownloadHandlerAudioClip dlHandler = (DownloadHandlerAudioClip)uwr.downloadHandler;

                    if (dlHandler.isDone) if (dlHandler.audioClip != null && _musicList.Contains(dlHandler.audioClip) == false) _musicList.Add(dlHandler.audioClip);
                }
            }
        }
    }

    private void Clear()
    {
        _musicList.Clear();
    }
}
