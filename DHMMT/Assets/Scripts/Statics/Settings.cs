using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    // Strings and methods for Settings

    // Audio
    public static (string VOLUME_KEY, float VolumeDefault) Volume = ("Volume", -20);

    public static float GetVolume() => PlayerPrefs.GetFloat(Volume.VOLUME_KEY, Volume.VolumeDefault);

    public static void SetVolume(float value)
    {
        PlayerPrefs.SetFloat(Volume.VOLUME_KEY, value);
        PlayerPrefs.Save();
    }

    // Mouse
    public static (string SENSITIVITY_KEY, float SensitivityDefault) Sensitivity = ("Sensitivity", 0.5f);
}
