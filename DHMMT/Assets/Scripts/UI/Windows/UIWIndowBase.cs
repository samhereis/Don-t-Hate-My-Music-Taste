using System;
using UnityEngine;

namespace UI.Window
{
    public abstract class UIWIndowBase : MonoBehaviour
    {
        protected static Action<UIWIndowBase> onAWindowOpen;

        [SerializeField] protected bool _isOpen = false;

        [Header("Components")]
        [SerializeField] protected WindowBehaviorBase _windowBehavior;

        public abstract void OnAWindowOpen(UIWIndowBase uIWIndow);

        public abstract void Enable();

#if UNITY_EDITOR
        [ContextMenu("Setup")] public abstract void Setup();
#endif
    }
}