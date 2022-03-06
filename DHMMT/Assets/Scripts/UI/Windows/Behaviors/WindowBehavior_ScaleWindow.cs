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
        [SerializeField] private Vector3 _upscaledValue = Vector3.one;
        [SerializeField] private Vector3 _downscaledValue = Vector3.zero;
        [SerializeField] private bool _revereseScaleAfterScale;

        protected override void Awake()
        {
            if (!_scaledObject) _scaledObject = transform;

            base.Awake();
        }

        public override async void Open()
        {
            await AsyncHelper.Delay(_openDelay);

            _windowEvents.onOpenStart?.Invoke();

            _scaledObject.DOScale(_upscaledValue, _openDuration).SetEase(_openEase).OnComplete(() =>
            {

                if (_revereseScaleAfterScale) _scaledObject.DOScale(_downscaledValue, _closeDuration);

                if (_disableEnableOnOpenClose) _scaledObject.gameObject.SetActive(true);

                _windowEvents.onOpenEnd?.Invoke();
            }).SetUpdate(true);
        }

        public override Task InstantlyClose()
        {
            if (!_scaledObject) _scaledObject = transform;

            _scaledObject.DOScale(_downscaledValue, 0);

            if (_disableEnableOnOpenClose) _scaledObject.gameObject.SetActive(false);

            _windowEvents.onInstantClose?.Invoke();

            return Task.CompletedTask;
        }

        public override async void Close()
        {
            await AsyncHelper.Delay(_closeDelay);

            _windowEvents.onCloseStart?.Invoke();

            _scaledObject.DOScale(_downscaledValue, _closeDuration).SetEase(_closeEase).OnComplete(() =>
            {

                if (_revereseScaleAfterScale) _scaledObject.DOScale(_upscaledValue, _openDuration);
                if (_disableEnableOnOpenClose) _scaledObject.gameObject.SetActive(false);

                _windowEvents.onCloseEnd?.Invoke();
            }).SetUpdate(true);
        }

        public void ScaleToOne(Transform obj)
        {
            _scaledObject.DOScale(1, _closeDuration).SetEase(_closeEase);
        }

        public void ScaleToZero(Transform obj)
        {
            _scaledObject.DOScale(0, _closeDuration).SetEase(_closeEase);
        }
    }
}