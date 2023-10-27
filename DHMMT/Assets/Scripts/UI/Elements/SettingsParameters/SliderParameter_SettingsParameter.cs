using Configs;
using ConstStrings;
using DI;
using Helpers;
using Interfaces;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements.SettingsParameters
{
    public class SliderParameter_SettingsParameter : MonoBehaviour, IInitializable<float>, IDIDependent
    {
        public Action<SliderParameter_SettingsParameter> onValueChanged;

        public float value => _value;
        public bool hasChanged => _value != _initialValue;

        [Header("Components")]
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _valueText;

        [Header("DI")]
        [DI(DIStrings.uiConfigs)][SerializeField] private UIConfigs _uiConfigs;

        [Header("Debug")]
        [SerializeField] private float _value = 0;
        [SerializeField] private float _initialValue = 0;

        [Header("In Editor")]
        [SerializeField] private string _label;
        [SerializeField] private TextMeshProUGUI _labelText;

        public void Initialize(float value)
        {
            (this as IDIDependent).LoadDependencies();

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

        [ContextMenu(nameof(UpdateLabel))]
        public void UpdateLabel()
        {
            _labelText.text = _label;
            _labelText.TrySetDirty();
        }

        private float RoundValue(float value)
        {
            return (float)Math.Round(value, 0);
        }
    }
}