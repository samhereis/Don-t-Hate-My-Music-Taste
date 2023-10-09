using DG.Tweening;
using UnityEngine;

namespace UI.UIAnimationElements
{
    public class UIAnimationElement_Position : UIAnimationElement_Base
    {
        [SerializeField] private RectTransform _holder;
        [SerializeField] private Vector3 _onOffPosition;

        private void OnDestroy()
        {
            _holder.DOKill();
        }

        public override void TurnOff(float? duration = null)
        {
            if (duration == null)
            {
                duration = _baseSettings.turnOffDuration;
            }

            if (duration.Value == 0)
            {
                _holder.position = _onOffPosition;
            }
            else
            {
                _holder.DOKill();
                _holder.DOLocalMove(_onOffPosition, duration.Value).SetEase(_baseSettings.ease);
            }
        }

        public override void TurnOn(float? duration = null)
        {
            if (duration == null)
            {
                duration = _baseSettings.turnOnDuration;
            }

            if (duration.Value == 0)
            {
                _holder.position = Vector3.zero;
            }
            else
            {
                _holder.DOKill();
                _holder.DOLocalMove(Vector3.zero, duration.Value).SetEase(_baseSettings.ease);
            }
        }
    }
}