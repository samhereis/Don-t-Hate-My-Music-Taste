using Helpers;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace UI
{
    public class LanguageManager : MonoBehaviour //TODO: replace playerPrefs with json
    {
        private async void Start()
        {
            while (LocalizationSettings.InitializationOperation.IsDone == false) await AsyncHelper.Delay();

            if (PlayerPrefs.HasKey("Language"))
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
}