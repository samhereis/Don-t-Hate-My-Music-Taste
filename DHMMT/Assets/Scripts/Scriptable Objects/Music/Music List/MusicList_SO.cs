using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "MusicList", menuName = "Scriptables/MusicList")]
public class MusicList_SO : ScriptableObject
{
    // Default music to play when player doesn't have music on computer

    public static string MusicFolderPath => $"{System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyMusic)}/DHMMT";

    [SerializeField] private List<AudioClip> _musicList = new List<AudioClip>();
    public List<AudioClip> musicList { get { if (_musicList.Count < 1) return _musicList; else return _defaultMusicList; } }

    [SerializeField] private List<AudioClip> _defaultMusicList;

    public async void LoadMusic()
    {
        _musicList.Clear();

        foreach (string file in System.IO.Directory.GetFiles(MusicFolderPath))
        {
            await ExtentionMethods.Delay();

            if (System.IO.File.Exists(file))
            {
                using (var uwr = UnityWebRequestMultimedia.GetAudioClip("file://" + file, AudioType.MPEG))
                {
                    ((DownloadHandlerAudioClip)uwr.downloadHandler).streamAudio = true;

                    //await uwr.SendWebRequest();

                    DownloadHandlerAudioClip dlHandler = (DownloadHandlerAudioClip)uwr.downloadHandler;

                    if (dlHandler.isDone)
                    {
                        AudioClip audioClip = dlHandler.audioClip;

                        if (audioClip != null && _musicList.Contains(dlHandler.audioClip) == false)
                        {
                            _musicList.Add(dlHandler.audioClip);
                        }
                    }
                }
            }
        }

        _musicList.RemoveNulls();

        if (_musicList.Count == 0)
        {
            _musicList.AddRange(_defaultMusicList);
        }
    }
}
