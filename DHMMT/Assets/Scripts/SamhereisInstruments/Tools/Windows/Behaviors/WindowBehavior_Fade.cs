using DG.Tweening;
using Helpers;
using System.Threading.Tasks;
using UnityEngine;

namespace UI.Window
{
    [RequireComponent(typeof(CanvasGroup))]
    [DisallowMultipleComponent]
    public class WindowBehavior_Fade : WindowBehaviorBase
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        [Header("Fade Settings")]
        [SerializeField] private float _upFadeValue = 1;
        [SerializeField] private float _downFadeValue = 0;
        [SerializeField] private bool _influenceIgnoreParentGroups = true;

        private void OnValidate()
        {
            if (_canvasGroup == null) _canvasGroup = GetComponent<CanvasGroup>();
        }

        protected override void Awake()
        {
            if (_canvasGroup == null) _canvasGroup = GetComponent<CanvasGroup>();

            base.Awake();
        }

        public override async void Open()
        {
            await AsyncHelper.Delay(_openDelay);

            if (_canvasGroup == null) _canvasGroup = GetComponent<CanvasGroup>();
            if (_canvasGroup == null)
            {
                Debug.LogError("No canvas group    " + gameObject.name, this);
                return;
            }

            _windowEvents.onOpenStart?.Invoke();
            SetActivateCanvas(true);

            _canvasGroup.DOFade(_upFadeValue, _openDuration).SetEase(_openEase).OnComplete(() => { _windowEvents.onOpenEnd?.Invoke(); }).SetUpdate(true);

            foreach (var window in _copyBehaviorTo) { await AsyncHelper.Delay(() => window.Open()); }
        }
        public override async void InstantlyClose()
        {
            if (_canvasGroup == null) _canvasGroup = GetComponent<CanvasGroup>();
            if (_canvasGroup == null)
            {
                Debug.LogError("No canvas group    " + gameObject.name, this);
            }

            _canvasGroup.alpha = _downFadeValue;

            SetActivateCanvas(false);

            foreach (var window in _copyBehaviorTo) { await AsyncHelper.Delay(() => window.InstantlyClose()); }

            _windowEvents.onInstantClose?.Invoke();
        }

        public override async void Close()
        {
            await AsyncHelper.Delay(_closeDelay);

            if (_canvasGroup == null) _canvasGroup = GetComponent<CanvasGroup>();
            if (_canvasGroup == null)
            {
                Debug.LogError("No canvas group    " + gameObject.name, this);
                return;
            }

            _windowEvents.onCloseStart?.Invoke();

            _canvasGroup.DOFade(_downFadeValue, _closeDuration).SetEase(_closeEase).OnComplete(() => { SetActivateCanvas(false); _windowEvents.onCloseEnd?.Invoke(); }).SetUpdate(true);

            foreach (var window in _copyBehaviorTo) { await AsyncHelper.Delay(() => window.Close()); }
        }

        public void SetActivateCanvas(bool value)
        {
            _canvasGroup.interactable = value;
            _canvasGroup.blocksRaycasts = value;

            if (_disableEnableOnOpenClose) _canvasGroup?.gameObject.SetActive(value);

            if (_influenceIgnoreParentGroups) _canvasGroup.ignoreParentGroups = value;
        }
    }
}