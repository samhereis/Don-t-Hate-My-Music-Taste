using Configs;
using ConstStrings;
using DI;
using Helpers;
using SO;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    public class ColorSetter : MonoBehaviour, IDIDependent
    {
        private static UIConfigs _uIConfigs;

        [Header("Components")]
        [SerializeField] private Dictionary<ColorSetUnitString, Graphic[]> _colorSetUnits = new Dictionary<ColorSetUnitString, Graphic[]>();

        private async void OnEnable()
        {
            while (DependencyInjector.isGLoballyInjected == false) { await AsyncHelper.DelayInt(20); }

            if (_uIConfigs == null)
            {
                _uIConfigs = DIBox.Get<UIConfigs>(DIStrings.uiConfigs);
            }

            SetColors();
        }

        private async void SetColors()
        {
            try
            {
                foreach (var colorSetUnit in _colorSetUnits)
                {
                    await AsyncHelper.Delay();

                    foreach (var graphic in colorSetUnit.Value)
                    {
                        graphic.color = _uIConfigs.colorSetUnits[colorSetUnit.Key];
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning("Error applying colors: " + ex);
            }
        }
    }
}