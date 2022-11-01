using DG.Tweening;
using Helpers;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Canvases
{
    public abstract class UIWIndowBase : MonoBehaviour
    {
        protected static Action<UIWIndowBase> onAWindowOpen;

        [SerializeField] protected BaseSettings baseSettings = new BaseSettings();

        protected virtual void Awake()
        {
            Setup();
            onAWindowOpen += OnAWindowOpen;
        }

        protected virtual void OnDestroy()
        {
            onAWindowOpen -= OnAWindowOpen;
        }

        public virtual void OnAWindowOpen(UIWIndowBase uIWIndow)
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
            if (baseSettings.notifyOthers == true) onAWindowOpen?.Invoke(this);

            baseSettings.canvasGroup.DOKill();

            if (duration == null) duration = baseSettings.animationDuration;

            if (baseSettings.enableDisable) gameObject.SetActive(true);
            baseSettings.canvasGroup.FadeUp(duration.Value);
        }

        public virtual void Disable(float? duration = null)
        {
            baseSettings.canvasGroup.DOKill();

            if (duration == null) duration = baseSettings.animationDuration;

            baseSettings.canvasGroup.FadeDown(duration.Value)?.OnComplete(() =>
            {
                if (baseSettings.enableDisable) gameObject.SetActive(false);
            }); ;
        }

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
        }
    }
}