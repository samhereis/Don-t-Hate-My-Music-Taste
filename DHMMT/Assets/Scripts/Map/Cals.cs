using System.Linq;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
public class Cals : MonoBehaviour
{
    public static Cals instance;
    /* Variables */
    public float bassSoundMult = 1, nextToBassSoundMult =1, middleSoundMult = 1, HighSoundMult = 1;    /* Frequencies */
    public float[] spectrumWidth;   /* range of sprectrums or width, usually used by  <<GetSpectrumData()>> and it also contained values set by <<GetSpectrumData()>> */
    public List<AudioClip> arrayOfSongs;    /* Array of Songs that should play in the scene */
    public ScriptableMusicList musicList;

    private AudioSource audioSource;    /* Playing audio */
    WWW www;
    public bool ShouldSearch;

    [SerializeField] MakeObjectsShake makeObjectsShake;

    void Awake()
    {
        spectrumWidth = new float[64];
        instance = this;
        audioSource = GetComponent<AudioSource>();      /*  get audio component that plays audio in the scene   */
    }
    void Start()
    {
        if(ShouldSearch)
        {
            foreach (string file in System.IO.Directory.GetFiles($"{System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyMusic)}/DHMMT"))
            {
                www = new WWW("file:///" + file);
                try
                {
                    if (www.GetAudioClip(true, true).GetType() == typeof(AudioClip))
                    {
                        arrayOfSongs.Add(www.GetAudioClip(true, true));
                    }
                }
                catch(System.Exception ex)
                {
                    Debug.Log(ex.Message);
                }
            }

            if (arrayOfSongs.Count < 1)
            {
                arrayOfSongs = musicList.musicList;
            }

            audioSource.clip = arrayOfSongs[Random.Range(0, arrayOfSongs.Count)];
        }
        
        InvokeRepeating("checkForAudio", 0f, 2f);       /* checkForAudio every * seconds */

        ExtentionMethods.SetWithNullCheck(ref makeObjectsShake, GetComponent<MakeObjectsShake>());
    }

    public void PauseMusic(bool pause)
    {
        if(pause)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
        }
    }

    void FixedUpdate()
    {   
        audioSource.GetSpectrumData(spectrumWidth, 0, FFTWindow.Blackman);      /* get spectrum of playing audio with sertain range specified by <<spectrumWidth>> and set to array called <<spectrumWidth>> */
    }

    void checkForAudio()    /* Check if any audio is playing */
    {
        if(!audioSource.isPlaying)
        {
            audioSource.clip = arrayOfSongs[Random.Range(0, arrayOfSongs.Count)];      /*Play random song from <<arrayOfSongs>>*/
            audioSource.Play();

            if(makeObjectsShake.enabled == false)
            {
                makeObjectsShake.enabled = true;
            }
        }
    }

    public float setSoundFreq(int s, int e, float mult)        /* method used for changing <<Frequencies>>, each frequency has their range for ex: bass should have range (0; 3) thats why we have start and end, and mult used for controlling the sensitivity*/
    {
        return spectrumWidth.ToList().GetRange(s, e).Average() * e * e * mult;
    }
    public float setSoundFreq(int s, int e, float mult, float min)        /* method used for changing <<Frequencies>>, each frequency has their range for ex: bass should have range (0; 3) thats why we have start and end, and mult used for controlling the sensitivity*/
    {
        return min + spectrumWidth.ToList().GetRange(s, e).Average() * e * e * mult;
    }
}
    
