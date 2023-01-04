using DG.Tweening;
using Helpers;
using System.Threading.Tasks;
using UnityEngine;

namespace UI.Window
{
    public class WindowBehavior_ScaleWindow : WindowBehaviorBase
    {
        [Header("Slace Settings")]
        [SerializeField] private Transform _scaledObject;
        [SerializeField] private Transform _currentScaledObject => _scaledObject != null ? _scaledObject : transform;
        [SerializeField] private Vector3 _upscaledValue = Vector3.one;
        [SerializeField] private Vector3 _downscaledValue = Vector3.zero;
        [SerializeField] private bool _revereseScaleAfterScale;

        protected override void Awake()
        {
            base.Awake();
        }

        public override async void Open()
        {
            await AsyncHelper.Delay(_openDelay);

            _windowEvents.onOpenStart?.Invoke();

            _currentScaledObject.DOScale(_upscaledValue, _openDuration).SetEase(_openEase).OnComplete(() =>
            {

                if (_revereseScaleAfterScale) _currentScaledObject.DOScale(_downscaledValue, _closeDuration);

                if (_disableEnableOnOpenClose) _currentScaledObject.gameObject.SetActive(true);

                _windowEvents.onOpenEnd?.Invoke();
            }).SetUpdate(true);
        }

        public override void InstantlyClose()
        {
            _currentScaledObject.DOScale(_downscaledValue, 0);

            if (_disableEnableOnOpenClose) _currentScaledObject.gameObject.SetActive(false);

            _windowEvents.onInstantClose?.Invoke();
        }

        public override async void Close()
        {
            await AsyncHelper.Delay(_closeDelay);

            _windowEvents.onCloseStart?.Invoke();

            _currentScaledObject.DOScale(_downscaledValue, _closeDuration).SetEase(_closeEase).OnComplete(() =>
            {
                if (_revereseScaleAfterScale) _currentScaledObject.DOScale(_upscaledValue, _openDuration);
                if (_disableEnableOnOpenClose) _currentScaledObject.gameObject.SetActive(false);

                _windowEvents.onCloseEnd?.Invoke();
            }).SetUpdate(true);
        }

        public void ScaleToOne(Transform obj)
        {
            _currentScaledObject.DOScale(1, _closeDuration).SetEase(_closeEase);
        }

        public void ScaleToZero(Transform obj)
        {
            _currentScaledObject.DOScale(0, _closeDuration).SetEase(_closeEase);
        }
    }
}