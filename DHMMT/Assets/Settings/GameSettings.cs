using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Scriptables.Gameplay;

public class GameSettings : MonoBehaviour
{
    // Controlls game setting 

    public static GameSettings instance;

    [Header("Sensitivity")]
    [SerializeField] private FloatSetting_SO _sensitivity;
    [SerializeField] private Slider _sensitivitySlider;

    [Header("Audio")]
    [SerializeField] private AudioMixer Mixer;
    [SerializeField] private FloatSetting_SO _volume;
    [SerializeField] private Slider _volumeSlider;

    private void Awake()
    {
        instance ??= this;
    }

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        // Initialize Audio
        _volumeSlider.value = _volume.currentValue;

        // Initialize Sensitivity
        _sensitivitySlider.value = _sensitivity.currentValue;
    }

    public void ApplyVolume()
    {
        Mixer.SetFloat(_volume.name, _volume.currentValue);
    }

    public void ChangeVolume(Slider slider)
    {
        _volume.SetData(slider.value);
        ApplyVolume();
    }

    public void ChangeSensitivity(Slider slider)
    {
        _sensitivity.SetData(slider.value);
    }

    public void DeleteAllGameData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
