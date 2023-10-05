using ConstStrings;
using DataClasses;
using Events;

namespace DI
{
    public class MainMenu_HCDI : HardCodeDependencyInjectorBase
    {
        public override void InjectEventsWithParameters()
        {
            DIBox.Add(new EventWithOneParameters<AScene_Extended>(Event_DIStrings.onASceneSelected), Event_DIStrings.onASceneSelected);
            DIBox.Add(new EventWithOneParameters<AScene_Extended>(Event_DIStrings.onASceneLoadRequested), Event_DIStrings.onASceneLoadRequested);
        }

        public override void InjecValueEvents()
        {

        }

        public override void ClearEventsWithParameters()
        {
            DIBox.Remove<EventWithOneParameters<AScene_Extended>>(Event_DIStrings.onASceneSelected);
            DIBox.Remove<EventWithOneParameters<AScene_Extended>>(Event_DIStrings.onASceneLoadRequested);
        }

        public override void ClearValueEvents()
        {

        }
    }
}