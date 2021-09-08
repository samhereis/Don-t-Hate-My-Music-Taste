using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageManager : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return LocalizationSettings.InitializationOperation;

        if(PlayerPrefs.HasKey("Language"))
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[PlayerPrefs.GetInt("Language")];
        }
        else
        {
            ChangeLanguage(2);
        }

    }

    public void ChangeLanguage(int index)
    {
        PlayerPrefs.SetInt("Language", index);
        PlayerPrefs.Save();
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[PlayerPrefs.GetInt("Language")];
    }
}
