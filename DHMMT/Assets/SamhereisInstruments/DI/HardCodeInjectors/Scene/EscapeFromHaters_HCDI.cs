using ConstStrings;
using Events;
using Identifiers;

namespace DI
{
    public class EscapeFromHaters_HCDI : HardCodeDependencyInjectorBase
    {
        public override void InjectEventsWithParameters()
        {
            DIBox.Add(new EventWithOneParameters<Exit_Identifier>(Event_DIStrings.onExitFound), Event_DIStrings.onExitFound);
        }

        public override void InjecValueEvents()
        {

        }

        public override void ClearEventsWithParameters()
        {
            DIBox.Remove<EventWithOneParameters<Exit_Identifier>>(Event_DIStrings.onExitFound);
        }

        public override void ClearValueEvents()
        {

        }
    }
}