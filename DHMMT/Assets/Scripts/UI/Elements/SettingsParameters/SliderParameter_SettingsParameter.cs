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
        [Header("Components")]
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _valueText;

        [Header("DI")]
        [DI(DIStrings.uiConfigs)][SerializeField] private UIConfigs _uiConfigs;

        [field: SerializeField, Header("Debug")] public float value { get; private set; } = 1;

        [Header("In Editor")]
        [SerializeField] private string _label;
        [SerializeField] private TextMeshProUGUI _labelText;

        public void Initialize(float type)
        {
            (this as IDIDependent).LoadDependencies();

            _slider.value = type;
            OnSliderValueChanged(type);
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
            value = (float)Math.Round(value, 3);

            this.value = value;
            _valueText.text = value.ToString();
        }

        [ContextMenu(nameof(UpdateLabel))]
        public void UpdateLabel()
        {
            _labelText.text = _label;
            _labelText.TrySetDirty();
        }
    }
}