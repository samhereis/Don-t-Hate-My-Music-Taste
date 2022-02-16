using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using UnityEngine.Events;
using Helpers;

namespace Sripts
{
    public abstract class UIWIndowBase : MonoBehaviour
    {
        protected static Action<UIWIndowBase> onAWindowOpen;

        protected abstract void Enable();

        protected abstract void Disable();

#if UNITY_EDITOR
        [ContextMenu("Setup")] public abstract void Setup();
#endif
    }
}