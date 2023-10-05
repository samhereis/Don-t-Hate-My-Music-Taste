using Helpers;
using System.Threading.Tasks;
using UI.Elements.SettingsParameters;
using UnityEngine;

namespace UI.Elements.SettingsTab
{
    public class Gameplay_SettingsTab : SettingsTabBase
    {
        [SerializeField] private SliderParameter_SettingsParameter _lookSensitivity_Slider;

        public override void Initialize()
        {
            base.Initialize();

            _lookSensitivity_Slider.Initialize(1);
        }

        public override async Task Apply()
        {
            await AsyncHelper.Delay();
        }
    }
}