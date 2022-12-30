using Helpers;
using Photon.Pun;
using System;
using UI.Window;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Canvases
{
    [RequireComponent(typeof(UICanvasEditorHelper))]
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasScaler))]
    [RequireComponent(typeof(GraphicRaycaster))]
    public abstract class CanvasBase : MonoBehaviourPunCallbacks
    {
        protected static Action<CanvasBase> onACanvasOpen;

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

        public virtual void OnACanvasOpen(CanvasBase UIWIndow)
        {
            if (UIWIndow != this) Disable();
        }

        public virtual void Enable(float? duration = null)
        {
            if (baseSettings.notifyOthers == true) onACanvasOpen?.Invoke(this);
            gameObject.SetActive(true);

            baseSettings.canvasGroup.FadeUp(duration != null ? duration.Value : baseSettings.animationDuration);
            baseSettings.defaultWindow?.Open();
        }

        public virtual void Disable(float? duration = null)
        {
            if (baseSettings.enableDisable == true) gameObject.SetActive(false);

            baseSettings.canvasGroup.FadeDown(duration != null ? duration.Value : baseSettings.animationDuration, completeCallback: () =>
            {
                if (baseSettings.enableDisable == true) gameObject.SetActive(false);
            });
        }

        [ContextMenu("Setup")]
        public virtual void Setup()
        {
            if (baseSettings.canvasGroup == null) baseSettings.canvasGroup = GetComponent<CanvasGroup>();

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
            public CanvasWindowBase defaultWindow;
        }
    }
}