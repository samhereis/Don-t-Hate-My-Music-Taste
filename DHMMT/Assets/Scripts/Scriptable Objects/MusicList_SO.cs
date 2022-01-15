using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "MusicList", menuName = "Scriptables/MusicList")]
public class MusicList_SO : ScriptableObject
{
    // Default music to play when player doesn't have music on computer

    public static string MusicFolderPath => $"{System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyMusic)}/DHMMT";

    public List<AudioClip> MusicList = new List<AudioClip>();

    public List<AudioClip> _defaultMusicList;

    public IEnumerator loadMusic()
    {
        MusicList.Clear();

        foreach (string file in System.IO.Directory.GetFiles(MusicFolderPath))
        {
            if (System.IO.File.Exists(file))
            {
                using (var uwr = UnityWebRequestMultimedia.GetAudioClip("file://" + file, AudioType.MPEG))
                {
                    ((DownloadHandlerAudioClip)uwr.downloadHandler).streamAudio = true;

                    yield return uwr.SendWebRequest();

                    DownloadHandlerAudioClip dlHandler = (DownloadHandlerAudioClip)uwr.downloadHandler;

                    if (dlHandler.isDone)
                    {
                        AudioClip audioClip = dlHandler.audioClip;

                        if (audioClip != null && MusicList.Contains(dlHandler.audioClip) == false)
                        {
                            MusicList.Add(dlHandler.audioClip);
                        }
                    }
                }
            }
        }

        MusicList.RemoveAll(x => !x);

        if(MusicList.Count == 0)
        {
            MusicList.AddRange(_defaultMusicList);
        }
    }
}
