using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicFolder : MonoBehaviour
{
    public string musicFolder;

    public List<AudioClip> arrayOfSongs;

    WWW www;

    private void Awake()
    {
        if (System.IO.Directory.Exists($"{System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyMusic)}/DHMMT") == false)
        {
            System.IO.Directory.CreateDirectory($"{System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyMusic)}/DHMMT");
        }

        foreach (string file in System.IO.Directory.GetFiles($"{System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyMusic)}/DHMMT"))
        {
            www = new WWW("file:///" + file);
            try
            {
                if(www.GetAudioClip(true, true).GetType() == typeof(AudioClip))
                {
                    arrayOfSongs.Add(www.GetAudioClip(true, true));
                }
            }
            catch (System.Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }

        if(arrayOfSongs.Count == 0)
        {
            gameObject.SetActive(true);

            musicFolder = $"{System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyMusic)}/DHMMT";
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
