using Configs;
using DG.Tweening;
using DI;
using Helpers;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements.SettingsTab
{
    public class Audio_SettingsTab : SettingsTabBase
    {
        [SerializeField] private Slider _lookSensitivity_Slider;

        public override async Task Apply()
        {
            await AsyncHelper.Delay();
        }
    }
}