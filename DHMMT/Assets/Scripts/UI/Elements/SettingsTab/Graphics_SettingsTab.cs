using Helpers;
using System.Threading.Tasks;

namespace UI.Elements.SettingsTab
{
    public class Graphics_SettingsTab : SettingsTabBase
    {
        public override bool hasChanged => false;

        public override async Task Apply()
        {
            await AsyncHelper.Delay();
        }

        public override async Task Restore()
        {
            await AsyncHelper.Delay();
        }
    }
}