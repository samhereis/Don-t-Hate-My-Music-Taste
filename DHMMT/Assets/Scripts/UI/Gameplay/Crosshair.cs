using DG.Tweening;
using Scriptables.Values;
using UnityEngine;

namespace UI
{
    public class Crosshair : MonoBehaviour
    {
        [SerializeField] private BoolValue_SO _isAiming;
        [SerializeField] private CanvasGroup _canvasGroup;

        private void OnEnable()
        {
            _isAiming.AddListener(SetActive);
        }

        private void OnDisable()
        {
            _isAiming.RemoveListener(SetActive);
        }

        public void SetActive(bool active)
        {
            _canvasGroup.DOKill();
            _canvasGroup.DOFade(active ? 1 : 0, 1);
        }
    }
}