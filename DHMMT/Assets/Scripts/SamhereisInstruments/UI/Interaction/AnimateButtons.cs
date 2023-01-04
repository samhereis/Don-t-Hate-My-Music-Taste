using DG.Tweening;
using Helpers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI.Interaction
{
    [DisallowMultipleComponent]
    public class AnimateButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private float _onOverScale = 1.1f;
        [SerializeField] private float _normaleScale = 1;

        [Header("Timing")]
        [SerializeField] private float _animationDuration = 0.5f;
        [SerializeField] private float _delayBetweenAnimations = 0.1f;

        public async void OnPointerEnter(PointerEventData eventData)
        {
            await AsyncHelper.Delay(_delayBetweenAnimations);
            transform.DOScale(_onOverScale, _animationDuration).SetEase(Ease.InOutBack);
        }
        public async void OnPointerExit(PointerEventData eventData)
        {
            await AsyncHelper.Delay(_delayBetweenAnimations);
            transform.DOScale(_normaleScale, _animationDuration).SetEase(Ease.InOutBack);
        }
    }
}