using Helpers;
using System.Threading.Tasks;

namespace UI.Elements.SettingsTab
{
    public class Graphics_SettingsTab : SettingsTabBase
    {
        public override bool hasChanged => false;

        public override async Task ApplyAsync()
        {
            await AsyncHelper.Delay();
        }

        public override async Task RestoreAsync()
        {
            await AsyncHelper.Delay();
        }
    }
}