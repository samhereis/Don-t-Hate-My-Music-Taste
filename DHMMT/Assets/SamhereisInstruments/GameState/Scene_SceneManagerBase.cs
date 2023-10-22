using Interfaces;
using UnityEngine;

namespace GameStates.SceneManagers
{
    public abstract class Scene_SceneManagerBase : MonoBehaviour, IInitializable, IClearable
    {
        public bool isDebugMode => _isDebugMode;

        public bool isInitialized { get; protected set; }

#if UNITY_EDITOR
        [SerializeField] protected bool _isDebugMode = false;
#else
        protected bool _isDebugMode => false;
#endif

        [ContextMenu(nameof(Initialize))]
        public virtual void Initialize()
        {

        }

        public virtual void Clear()
        {

        }

        public bool GetCanInitializeWithDI()
        {
            return false;
        }
    }
}