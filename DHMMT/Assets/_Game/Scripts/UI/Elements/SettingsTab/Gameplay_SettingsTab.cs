using ConstStrings;
using DependencyInjection;
using Helpers;
using Settings;
using System.Collections;
using System.Threading.Tasks;
using UI.Elements.SettingsParameters;
using UnityEngine;

namespace UI.Elements.SettingsTab
{
    public class Gameplay_SettingsTab : SettingsTabBase
    {
        [Header("Sliders")]
        [SerializeField] private SliderParameter_SettingsParameter _fieldOfView_Slider;
        [SerializeField] private SliderParameter_SettingsParameter _mouseSensitivity_Slider;

        [Header("DI")]
        [Inject(Savables_ConstStrings.fieldOfView_Settings)][SerializeField] private FloatSavable_SO _fieldOfView_Settings;
        [Inject(Savables_ConstStrings.mouseSensitivity_Settings)][SerializeField] private FloatSavable_SO _mouseSensitivity_Settings;

        public override async void Initialize()
        {
            base.Initialize();

            DependencyContext.diBox.InjectDataTo(this);

            await RestoreAsync();
        }

        public override async Task OpenAsync()
        {
            await base.OpenAsync();

            StartCoroutine(UpdateAndHandleHasChangedStatus());
        }

        public override async Task CloseAsync()
        {
            await base.CloseAsync();

            StopCoroutine(UpdateAndHandleHasChangedStatus());
        }

        public override async Task ApplyAsync()
        {
            await AsyncHelper.Skip();

            _fieldOfView_Settings.SetData(_fieldOfView_Slider.value);
            _mouseSensitivity_Settings.SetData(_mouseSensitivity_Slider.value);

            await RestoreAsync();
        }

        public override async Task RestoreAsync()
        {
            await AsyncHelper.Skip();

            SetSliders();
        }

        private void SetSliders()
        {
            _fieldOfView_Slider.SetSliderValues(0f, 100f);
            _fieldOfView_Slider.Initialize(_fieldOfView_Settings.currentValue);

            _mouseSensitivity_Slider.SetSliderValues(0f, 100f);
            _mouseSensitivity_Slider.Initialize(_mouseSensitivity_Settings.currentValue);
        }

        protected IEnumerator UpdateAndHandleHasChangedStatus()
        {
            while (gameObject.activeSelf)
            {
                yield return new WaitForEndOfFrame();

                if (_fieldOfView_Slider.hasChanged == true) { _baseSettings.hasChanged = true; continue; }
                if (_mouseSensitivity_Slider.hasChanged == true) { _baseSettings.hasChanged = true; continue; }

                _baseSettings.hasChanged = false;
            }
        }
    }
}