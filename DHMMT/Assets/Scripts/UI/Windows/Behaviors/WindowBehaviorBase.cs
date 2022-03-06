using DG.Tweening;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace UI.Window
{
    [DisallowMultipleComponent]
    public abstract class WindowBehaviorBase : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] protected WindowEvents _windowEvents;

        [Header("Settings")]
        [SerializeField] protected WindowBehaviorBase[] _copyBehaviorTo;
        [SerializeField] protected bool _autoCloseInstantlyOnAwake;
        [SerializeField] protected bool _disableEnableOnOpenClose = false;
        [SerializeField] protected bool _autoAnimateOnEnable;

        [Header(" OpenSettings")]
        [SerializeField] protected Ease _openEase = Ease.OutBack;
        [SerializeField] protected float _openDuration = 0.5f;
        [SerializeField] protected float _openDelay = 0;

        [Header(" CloseSettings")]
        [SerializeField] protected Ease _closeEase = Ease.InBack;
        [SerializeField] protected float _closeDuration = 0.5f;
        [SerializeField] protected float _closeDelay = 0;

        protected virtual void Awake()
        {
            if (_autoCloseInstantlyOnAwake) InstantlyClose();
        }

        protected virtual async void OnEnable()
        {
            if (_autoAnimateOnEnable)
            {
                await InstantlyClose();

                Open();
            }
        }

        protected virtual void OnDisable()
        {
            if (_autoAnimateOnEnable) Close();
        }

        public abstract void Open();
        public abstract Task InstantlyClose();
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

    [SerializeField] private UnityEvent _onInstantClose = new UnityEvent();
    public UnityEvent onInstantClose => _onInstantClose;

    [SerializeField] private UnityEvent _onCloseStart = new UnityEvent();
    public UnityEvent onCloseStart => _onCloseStart;

    [SerializeField] private UnityEvent _onCloseEnd = new UnityEvent();
    public UnityEvent onCloseEnd => _onCloseEnd;
}