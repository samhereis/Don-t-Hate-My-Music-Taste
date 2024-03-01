using Configs;
using DependencyInjection;
using Interfaces;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements.SettingsParameters
{
    public class SliderParameter_SettingsParameter : MonoBehaviour, IInitializable<float>, INeedDependencyInjection
    {
        public Action<SliderParameter_SettingsParameter> onValueChanged;

        public float value => _value;
        public bool hasChanged => _value != _initialValue;

        [Header("Components")]
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _valueText;

        [Header("DI")]
        private UIConfigs _uiConfigs;

        [Header("Debug")]
        [SerializeField] private float _value = 0;
        [SerializeField] private float _initialValue = 0;

        public void Initialize(float value)
        {
            DependencyContext.diBox.InjectDataTo(this);

            _initialValue = RoundValue(value);

            _slider.value = _initialValue;
            OnSliderValueChanged(_initialValue);
        }

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }

        private void OnSliderValueChanged(float value)
        {
            _value = RoundValue(value);
            _valueText.text = _value.ToString();

            onValueChanged?.Invoke(this);
        }

        public void SetSliderValues(float minValue, float maxValue)
        {
            _slider.minValue = minValue;
            _slider.maxValue = maxValue;
        }

        private float RoundValue(float value)
        {
            return (float)Math.Round(value, 2);
        }
    }
}