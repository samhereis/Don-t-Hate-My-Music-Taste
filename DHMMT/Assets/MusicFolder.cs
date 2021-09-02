using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicFolder : MonoBehaviour
{
    public string musicFolder;
    public int musicCount;

    public List<AudioClip> arrayOfSongs;

    WWW www;

    [SerializeField] GameObject Found;
    [SerializeField] GameObject NotFound;

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
            musicFolder = $"{System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyMusic)}/DHMMT";

            NotFound.SetActive(true);
        }
        else if(arrayOfSongs.Count > 0)
        {
            musicCount = arrayOfSongs.Count;

            Found.SetActive(true);
        }
    }
}
