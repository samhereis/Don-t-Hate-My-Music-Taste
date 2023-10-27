using UnityEngine;

namespace DI
{
    public abstract class HardCodeDependencyInjectorBase : MonoBehaviour
    {
        public virtual void Inject()
        {
            InjectEventsWithParameters();
            InjecValueEvents();
        }

        public virtual void Clear()
        {
            ClearEventsWithParameters();
            ClearValueEvents();
        }

        public virtual void InjectEventsWithParameters() { }
        public virtual void InjecValueEvents() { }

        public virtual void ClearEventsWithParameters() { }
        public virtual void ClearValueEvents() { }
    }
}