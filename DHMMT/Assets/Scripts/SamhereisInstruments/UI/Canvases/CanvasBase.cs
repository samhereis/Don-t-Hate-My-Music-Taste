using Helpers;
using System;
using UI.Window;
using UnityEngine;

namespace UI.Canvases
{
    public abstract class CanvasBase : MonoBehaviour
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
            Debug.Log(gameObject.name + " Enable ");
            if (baseSettings.notifyOthers == true) onACanvasOpen?.Invoke(this);
            if (baseSettings.enableDisable == true) gameObject.SetActive(true);

            if (baseSettings.canvasGroup != null)
            {
                baseSettings.canvasGroup.FadeUp(duration != null ? duration.Value : baseSettings.animationDuration);
            }
            else
            {
                baseSettings.windowBehavior?.Open();
            }
        }

        public virtual void Disable(float? duration = null)
        {
            Debug.Log(gameObject.name + " Disable ");

            if (baseSettings.canvasGroup != null)
            {
                baseSettings.canvasGroup.FadeDown(duration != null ? duration.Value : baseSettings.animationDuration, completeCallback: () =>
                {
                    if (baseSettings.enableDisable == true) gameObject.SetActive(false);
                });
            }
            else
            {
                baseSettings.windowBehavior?.Close();
            }
        }

        [ContextMenu("Setup")]
        public virtual void Setup()
        {
            if (baseSettings.canvasGroup == null) baseSettings.canvasGroup = GetComponent<CanvasGroup>();
            if (baseSettings.windowBehavior == null) baseSettings.windowBehavior = GetComponent<WindowBehaviorBase>();

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
            public WindowBehaviorBase windowBehavior;
        }
    }
}