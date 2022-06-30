using Samhereis.UI.Window;
using System;
using UnityEngine;

namespace Samhereis.UI
{
    public abstract class UIWIndowBase : MonoBehaviour
    {
        protected static Action<UIWIndowBase> onAWindowOpen;

        [SerializeField] protected bool _isOpen = false;

        [Header("Components")]
        [SerializeField] protected WindowBehaviorBase _windowBehavior;

        protected virtual void Awake()
        {
            if (_windowBehavior == null) _windowBehavior = GetComponent<WindowBehaviorBase>();

            if (_windowBehavior == null) Debug.LogWarning("_windowBehavior is null", this);
        }

        public abstract void OnAWindowOpen(UIWIndowBase uIWIndow);

        public abstract void Enable();

#if UNITY_EDITOR
        [ContextMenu("Setup")] public abstract void Setup();
#endif
    }
}