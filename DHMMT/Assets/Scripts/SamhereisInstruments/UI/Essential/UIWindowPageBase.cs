using DG.Tweening;
using Helpers;
using System;
using UnityEngine;

namespace UI.Canvases
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UIWindowPageBase : MonoBehaviour
    {
        [SerializeField] protected WindowPageBaseSettings _baseSettings = new WindowPageBaseSettings();

        public virtual void Open(float? duration = null)
        {
            if (duration == null) duration = _baseSettings._animationDuration;

            _baseSettings._canvasGroup.DOKill();
            if (_baseSettings.enableDisable) gameObject.SetActive(true);
            _baseSettings._canvasGroup.FadeUp(duration.Value);
        }

        public virtual void Close(float? duration = null)
        {
            if (duration == null) duration = _baseSettings._animationDuration;

            _baseSettings._canvasGroup.FadeDown(duration.Value, completeCallback: () =>
            {
                if (_baseSettings.enableDisable) gameObject.SetActive(false);
            });
        }

        [ContextMenu(nameof(Setup))]
        public void Setup()
        {
            if (_baseSettings._parentCanvas == null) _baseSettings._parentCanvas = GetComponentInParent<UICanvasBase>();
            if (_baseSettings._canvasGroup == null) _baseSettings._canvasGroup = GetComponent<CanvasGroup>();
        }

        [Serializable]
        protected class WindowPageBaseSettings
        {
            public UICanvasBase _parentCanvas;
            public CanvasGroup _canvasGroup;
            public float _animationDuration = 0.5f;
            public bool enableDisable = true;
        }
    }
}
