using Samhereis.Helpers;
using System;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Samhereis.UI
{
    public class LanguageManager : MonoBehaviour
    {
        private async void Start()
        {
            while (LocalizationSettings.InitializationOperation.IsDone == false) await AsyncHelper.Delay();

            if (PlayerPrefs.HasKey("Language")) UpdateLocale(); else ChangeLanguage(2);
        }

        public void ChangeLanguage(int index)
        {
            PlayerPrefs.SetInt("Language", index);
            PlayerPrefs.Save();

            UpdateLocale();
        }

        private void UpdateLocale()
        {
            try { LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[PlayerPrefs.GetInt("Language")]; }
            catch(Exception ex) { Debug.LogWarning(ex); }
            
        }
    }
}