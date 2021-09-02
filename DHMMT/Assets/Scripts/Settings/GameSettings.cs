using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    public static GameSettings instance;

    [Header("Sensitivity")]
    public float sensitivity;

    [Header("Audio")]
    public AudioMixer Mixer;

    [Header("Settings")]
    [SerializeField] Slider Volume;
    [SerializeField] Slider Sensitivity;

    void Awake()
    {
        if(PlayerPrefs.GetFloat(nameof(Sensitivity)) == 0f)
        {
            ChangeSensitivity(Volume);
        }

        Volume.value = PlayerPrefs.GetFloat(nameof(Volume));
        Sensitivity.value = PlayerPrefs.GetFloat(nameof(Sensitivity));
    }

    void OnEnable()
    {
        ExtentionMethods.SetWithNullCheck(ref instance, this);
    }

    public void ChangeVolume(Slider slider)
    {
        Mixer.SetFloat(nameof(Volume), slider.value);
        PlayerPrefs.SetFloat(nameof(Volume), slider.value);
        PlayerPrefs.Save();
    }

    public void ChangeSensitivity(Slider slider)
    {
        sensitivity = slider.value;

        PlayerPrefs.SetFloat(nameof(Sensitivity), slider.value);
        PlayerPrefs.Save();
    }
}
