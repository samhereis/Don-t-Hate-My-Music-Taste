using ConstStrings;
using DI;
using Helpers;
using Settings;
using SO.DataHolders;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI.Elements.SettingsParameters;
using UnityEngine;

namespace UI.Elements.SettingsTab
{
    public class Audio_SettingsTab : SettingsTabBase
    {
        public override bool hasChanged => false;

        [SerializeField] private SliderParameter_SettingsParameter _masterVolume_Slider;
        [SerializeField] private SliderParameter_SettingsParameter _musicVolume_Slider;
        [SerializeField] private SliderParameter_SettingsParameter _effectsVolume_Slider;

        [Header("Settings")]
        [SerializeField] private float _minVolume = 50f;

        [Header("DI")]
        [DI("")][SerializeField] private AudioMixerDataHolder _audioMixer;

        [Space(20)]

        [DI(DIStrings.masterVolume_Settings)][SerializeField] private FloatSavable_SO _masterVolume_Settings;
        [DI(DIStrings.musicVolume_Settings)][SerializeField] private FloatSavable_SO _musicVolume_Settings;
        [DI(DIStrings.effectsVolume_Settings)][SerializeField] private FloatSavable_SO _effectsVolume_Settings;

        private Dictionary<SliderParameter_SettingsParameter, FloatSavable_SO> _volumeSettings = new Dictionary<SliderParameter_SettingsParameter, FloatSavable_SO>();

        public override void Initialize()
        {
            base.Initialize();

            _volumeSettings = new Dictionary<SliderParameter_SettingsParameter, FloatSavable_SO>
            {
                { _masterVolume_Slider, _masterVolume_Settings },
                { _musicVolume_Slider, _musicVolume_Settings },
                { _effectsVolume_Slider, _effectsVolume_Settings }
            };

            ApplyVolumes();
        }

        public override async Task OpenAsync()
        {
            await base.OpenAsync();

            SyncSlidersWithSettings();

            foreach (var volumeSetting in _volumeSettings)
            {
                volumeSetting.Key.SetSliderValues(0, 100);
                volumeSetting.Key.Initialize(volumeSetting.Value.currentValue);

                volumeSetting.Key.onValueChanged -= SynSettingsWithSlider;
                volumeSetting.Key.onValueChanged += SynSettingsWithSlider;
            }
        }

        public override async Task CloseAsync()
        {
            await base.CloseAsync();

            foreach (var volumeSetting in _volumeSettings)
            {
                volumeSetting.Key.onValueChanged -= SynSettingsWithSlider;
            }
        }

        private void SynSettingsWithSlider(SliderParameter_SettingsParameter slider)
        {
            _volumeSettings[slider].SetData(slider.value);
            ApplyVolumes();
        }

        private void SyncSlidersWithSettings()
        {
            _masterVolume_Slider.Initialize(_masterVolume_Settings.currentValue);
            _musicVolume_Slider.Initialize(_musicVolume_Settings.currentValue);
            _effectsVolume_Slider.Initialize(_effectsVolume_Settings.currentValue);
        }

        public override async Task ApplyAsync()
        {
            await AsyncHelper.Delay();
        }

        private void ApplyVolumes()
        {
            _audioMixer.data.SetFloat(DIStrings.masterVolume_Settings, GetVolume(_masterVolume_Settings));
            _audioMixer.data.SetFloat(DIStrings.musicVolume_Settings, GetVolume(_musicVolume_Settings));
            _audioMixer.data.SetFloat(DIStrings.effectsVolume_Settings, GetVolume(_effectsVolume_Settings));
        }

        public override async Task RestoreAsync()
        {
            await AsyncHelper.Delay();
        }

        private float GetVolume(FloatSavable_SO volume)
        {
            if (volume == null) { return -25; }

            var percent = NumberHelper.GetPercentageOf100(volume.currentValue, 100f);
            var volumeOfAudioMixer = NumberHelper.GetNumberFromPercentage(_minVolume, percent);

            var resultVolume = -(_minVolume - volumeOfAudioMixer);

            if (resultVolume <= -Mathf.Abs(_minVolume)) { resultVolume = -80; }

            return resultVolume;
        }
    }
}