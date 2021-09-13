using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayingMusicData : MonoBehaviour
{
    // Gets and holds data from a playing music

    public static PlayingMusicData instance;

    [Header("Reactors")]
    public float BassSoundMult = 1;
    public float NextToBassSoundMult = 1;
    public float MiddleSoundMult = 1;
    public float HighSoundMult = 1;

    [Header("Music's gotten data")]
    public float[] SpectrumWidth;
    public List<AudioClip> ArrayOfSongs;
    public ScriptableMusicList MusicList;

    [Header("Search for music data")]
    private WWW _www;
    public bool ShouldSearch;

    [Header("Components")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private MakeObjectsShake _makeObjectsShake;

    private List<string> FoundMusic { get { return System.IO.Directory.GetFiles($"{System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyMusic)}/DHMMT").ToList<string>(); } }

    private void Awake()
    {
        SpectrumWidth = new float[64];
        instance = this;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (ShouldSearch && FoundMusic.Count > 0)
        {
            foreach (string file in FoundMusic)
            {
                if (_www.GetAudioClip(true, true).GetType() == typeof(AudioClip))
                {
                    ArrayOfSongs.Add(_www.GetAudioClip(true, true));
                }
            }
        }
        else
        {
            ArrayOfSongs = MusicList.MusicList;

            _audioSource.clip = ArrayOfSongs[Random.Range(0, ArrayOfSongs.Count)];
        }

        InvokeRepeating("checkForAudio", 0f, 2f);       /* checkForAudio if music is playing every * seconds */

        ExtentionMethods.SetWithNullCheck(ref _makeObjectsShake, GetComponent<MakeObjectsShake>());
    }

    public void PauseMusic(bool pause)
    {
        if (pause)
        {
            _audioSource.Pause();
        }
        else
        {
            _audioSource.UnPause();
        }
    }

    private void FixedUpdate()
    {
        _audioSource.GetSpectrumData(SpectrumWidth, 0, FFTWindow.Blackman);      /* get spectrum of playing audio with sertain range specified by <<spectrumWidth>> and set to array called <<spectrumWidth>> */
    }

    private void checkForAudio()    /* Check if any audio is playing */
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.clip = ArrayOfSongs[Random.Range(0, ArrayOfSongs.Count)];      /*Play random song from <<arrayOfSongs>>*/
            _audioSource.Play();

            if (_makeObjectsShake.enabled == false)
            {
                _makeObjectsShake.enabled = true;
            }
        }
    }

    public float setSoundFreq(int s, int e, float mult)        /* method used for changing <<Frequencies>>, each frequency has their range for ex: bass should have range (0; 3) thats why we have start and end, and mult used for controlling the sensitivity*/
    {
        return SpectrumWidth.ToList().GetRange(s, e).Average() * e * e * mult;
    }

    public float setSoundFreq(int s, int e, float mult, float min)        /* method used for changing <<Frequencies>>, each frequency has their range for ex: bass should have range (0; 3) thats why we have start and end, and mult used for controlling the sensitivity*/
    {
        return min + SpectrumWidth.ToList().GetRange(s, e).Average() * e * e * mult;
    }
}

