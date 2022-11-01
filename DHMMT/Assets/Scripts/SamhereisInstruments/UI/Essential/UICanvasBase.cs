using Helpers;
using System;
using UnityEngine;

namespace UI.Canvases
{
    public abstract class UICanvasBase : MonoBehaviour
    {
        protected static Action<UICanvasBase> onACanvasOpen;

        [SerializeField] protected BaseSettings baseSettings = new BaseSettings();

        protected virtual void OnValidate()
        {
            Setup();
        }

        protected virtual void Awake()
        {
            onACanvasOpen += OnACanvasOpen;
        }

        protected virtual void OnDestroy()
        {
            onACanvasOpen -= OnACanvasOpen;
        }

        public void Open()
        {
            Enable();
        }

        public void Close()
        {
            Disable();
        }

        public virtual void OnACanvasOpen(UICanvasBase UIWIndow)
        {
            if (UIWIndow != this) Disable();
        }

        public virtual void Enable(float? duration = null)
        {
            if (baseSettings.notifyOthers == true) onACanvasOpen?.Invoke(this);

            if (baseSettings.enableDisable == true) gameObject.SetActive(true);

            baseSettings.canvasGroup.FadeUp(duration != null ? duration.Value : baseSettings.animationDuration);
        }

        public virtual void Disable(float? duration = null)
        {
            baseSettings.canvasGroup.FadeDown(duration != null ? duration.Value : baseSettings.animationDuration, completeCallback: () =>
            {
                if (baseSettings.enableDisable == true) gameObject.SetActive(false);
            });
        }

        [ContextMenu("Setup")]
        public virtual void Setup()
        {

            this.TrySetDirty();
        }

        [System.Serializable]
        protected class BaseSettings
        {
            [Header("Settings")]
            public bool enableDisable = true;
            public bool notifyOthers = true;
            public float animationDuration = 0.5f;

            [Header("Components")]
            public CanvasGroup canvasGroup;
        }
    }
}