using DG.Tweening;
using Helpers;
using Photon.Pun;
using System;
using UI.Canvases;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Window
{
    [RequireComponent(typeof(UIWindowEditorHelper))]
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class CanvasWindowBase : MonoBehaviour
    {
        public static Action<CanvasWindowBase> onAWindowOpen;

        [SerializeField] protected BaseSettings baseSettings = new BaseSettings();

        protected virtual void OnValidate()
        {
            Setup();
        }

        protected virtual void Awake()
        {
            Setup();
            onAWindowOpen += OnAWindowOpen;
        }

        protected virtual void OnDestroy()
        {
            onAWindowOpen -= OnAWindowOpen;
        }

        public virtual void OnAWindowOpen(CanvasWindowBase uIWIndow)
        {
            if (uIWIndow != this) Disable();
        }

        public void Open()
        {
            Enable();
        }

        public void Close()
        {
            Disable();
        }

        public virtual void Enable(float? duration = null)
        {
            if (baseSettings.isOpen == false)
            {
                baseSettings.isOpen = true;
                
                baseSettings.parentCanvas?.EnsureIsOpen();

                if (baseSettings.notifyOthers == true) onAWindowOpen?.Invoke(this);

                baseSettings.canvasGroup.DOKill();

                if (duration == null) duration = baseSettings.animationDuration;

                if (baseSettings.enableDisable) gameObject?.SetActive(true);
                baseSettings.canvasGroup?.FadeUp(duration.Value);
            }
        }

        public virtual void Disable(float? duration = null)
        {
            baseSettings.canvasGroup.DOKill();

            if (duration == null) duration = baseSettings.animationDuration;

            baseSettings.canvasGroup.FadeDown(duration.Value)?.OnComplete(() =>
            {
                if (baseSettings.enableDisable) gameObject.SetActive(false);
            });

            baseSettings.isOpen = false;
        }

        public virtual void Setup()
        {
            if (baseSettings.canvasGroup == null) baseSettings.canvasGroup = GetComponent<CanvasGroup>();
            if (baseSettings.parentCanvas == null) baseSettings.parentCanvas = GetComponentInParent<CanvasBase>(true);
            this.TrySetDirty();
        }

        [System.Serializable]
        protected class BaseSettings
        {
            public bool isOpen;

            [Header("Settings")]
            public bool enableDisable = true;
            public bool notifyOthers = true;
            public float animationDuration = 0.5f;

            [Header("Components")]
            public CanvasGroup canvasGroup;
            public CanvasBase parentCanvas;
        }
    }
}