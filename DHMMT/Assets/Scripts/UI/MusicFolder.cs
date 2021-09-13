using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicFolder : MonoBehaviour
{
    // Check if player has music on computer

    public string MusicFolderPath;
    public int MusicCount;

    public List<AudioClip> ArrayOfSongs;

    private WWW www;

    [SerializeField] private GameObject _found;
    [SerializeField] private GameObject _notFound;

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
                    ArrayOfSongs.Add(www.GetAudioClip(true, true));
                }
            }
            catch (System.Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }

        if(ArrayOfSongs.Count == 0)
        {
            MusicFolderPath = $"{System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyMusic)}/DHMMT";

            _notFound.SetActive(true);
        }
        else if(ArrayOfSongs.Count > 0)
        {
            MusicCount = ArrayOfSongs.Count;

            _found.SetActive(true);
        }
    }
}
