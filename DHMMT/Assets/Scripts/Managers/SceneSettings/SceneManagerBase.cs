using Interfaces;
using UnityEngine;

namespace Managers.SceneManagers
{
    public abstract class SceneManagerBase : MonoBehaviour, IInitializable, IClearable
    {
        public bool isInitialized { get; protected set; }

        public abstract void Initialize();

        public abstract void Clear();

        public bool GetCanInitializeWithDI()
        {
            return false;
        }
    }
}