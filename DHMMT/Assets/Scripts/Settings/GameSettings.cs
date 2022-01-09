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
        print("Awake");

        // Initialize Audio
        _volumeSlider.value = Settings.GetVolume();
        ChangeVolume(_volumeSlider);

        // Initialize Sensitivity
        _sensitivitySlider.value = PlayerPrefs.GetFloat(Settings.Sensitivity.SENSITIVITY_KEY);
        ChangeSensitivity(_sensitivitySlider);
    }

    private void OnEnable()
    {
        ExtentionMethods.SetWithNullCheck(ref instance, this);
    }

    public void ChangeVolume(Slider slider)
    {
        Mixer.SetFloat(Settings.Volume.VOLUME_KEY, slider.value);

        Settings.SetVolume(slider.value);
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
