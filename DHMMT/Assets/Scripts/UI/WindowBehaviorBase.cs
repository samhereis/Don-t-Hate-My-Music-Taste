using UnityEngine;
using UnityEngine.Events;

namespace UI.Window
{
    [DisallowMultipleComponent]
    public abstract class WindowBehaviorBase : MonoBehaviour
    {
        [SerializeField] protected WindowEvents _windowEvents;

        public abstract void Open();
        public abstract void Close();
    }
}

[System.Serializable]
public class WindowEvents
{
    [SerializeField] private UnityEvent _onOpenStart = new UnityEvent();
    public UnityEvent onOpenStart => _onOpenStart;

    [SerializeField] private UnityEvent _onOpenEnd = new UnityEvent();
    public UnityEvent onOpenEnd => _onOpenEnd;

    [SerializeField] private UnityEvent _onCloseStart = new UnityEvent();
    public UnityEvent onCloseStart => _onCloseStart;

    [SerializeField] private UnityEvent _onCloseEnd = new UnityEvent();
    public UnityEvent onCloseEnd => _onCloseEnd;
}