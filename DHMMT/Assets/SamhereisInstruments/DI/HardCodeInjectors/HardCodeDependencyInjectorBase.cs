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

        public abstract void InjectEventsWithParameters();
        public abstract void InjecValueEvents();

        public abstract void ClearEventsWithParameters();
        public abstract void ClearValueEvents();
    }
}