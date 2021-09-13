using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    // Controlls game setting 

    public static GameSettings instance;

    [Header("Sensitivity")]
    public float SensitivityValue;

    [Header("Audio")]
    public AudioMixer Mixer;

    [Header("Settings")]
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private Slider _sensitivitySlider;

    private void Awake()
    {
        // Initialize Audio
        if (PlayerPrefs.HasKey(Settings.Volume.VOLUME_KEY) == false)
        {
            _volumeSlider.value = Settings.Volume.VolumeDefault;
            ChangeVolume(_volumeSlider);
        }
        else
        {
            _volumeSlider.value = PlayerPrefs.GetFloat(Settings.Volume.VOLUME_KEY);
            ChangeVolume(_volumeSlider);
        }

        // Initialize Sensitivity
        if (PlayerPrefs.HasKey(Settings.Sensitivity.SENSITIVITY_KEY) == false)
        {
            _sensitivitySlider.value = Settings.Sensitivity.SensitivityDefault;
            ChangeSensitivity(_sensitivitySlider);
        }
        else
        {
            _sensitivitySlider.value = PlayerPrefs.GetFloat(Settings.Sensitivity.SENSITIVITY_KEY);
            ChangeSensitivity(_sensitivitySlider);
        }
    }

    private void OnEnable()
    {
        ExtentionMethods.SetWithNullCheck(ref instance, this);
    }

    public void ChangeVolume(Slider slider)
    {
        Mixer.SetFloat(Settings.Volume.VOLUME_KEY, slider.value);
        PlayerPrefs.SetFloat(Settings.Volume.VOLUME_KEY, slider.value);
        PlayerPrefs.Save();
    }

    public void ChangeSensitivity(Slider slider)
    {
        SensitivityValue = slider.value;

        PlayerPrefs.SetFloat(Settings.Sensitivity.SENSITIVITY_KEY, slider.value);
        PlayerPrefs.Save();
    }

    public void DeleteAllGameData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
