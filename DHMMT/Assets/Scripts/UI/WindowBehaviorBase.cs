using UnityEngine;
using UnityEngine.Events;

namespace UI.Window
{
    [DisallowMultipleComponent]
    public abstract class WindowBehaviorBase : MonoBehaviour
    {
        public UnityEvent onOpenStart = new UnityEvent();
        public UnityEvent onOpenEnd = new UnityEvent();

        public UnityEvent onCloseStart = new UnityEvent();
        public UnityEvent onCloseEnd = new UnityEvent();

        public abstract void Open();
        public abstract void Close();
    }
}
