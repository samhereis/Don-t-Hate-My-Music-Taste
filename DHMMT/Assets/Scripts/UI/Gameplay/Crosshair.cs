using DG.Tweening;
using Helpers;
using UnityEngine;
using Values;

namespace UI
{
    public class Crosshair : MonoBehaviour
    {
        [SerializeField] private BoolValue_SO _isAiming;
        [SerializeField] private CanvasGroup _canvasGroup;

        [Header("Settings")]
        [SerializeField] private float _fadeDuration = 0.5f;

        private async void OnEnable()
        {
            if (_isAiming == null) _isAiming = await AddressablesHelper.GetAssetAsync<BoolValue_SO>("IsAiming");
            _isAiming.AddListener(SetActive);
        }

        private void OnDisable()
        {
            _isAiming.RemoveListener(SetActive);
        }

        public void SetActive(bool isAiming)
        {
            _canvasGroup.DOKill();

            if (isAiming)
            {
                _canvasGroup.DOFade(0, _fadeDuration);
                _canvasGroup.transform.ScaleDown(_fadeDuration);
            }
            else
            {
                _canvasGroup.DOFade(1, _fadeDuration);
                _canvasGroup.transform.ScaleUp(_fadeDuration);
            }
        }
    }
}