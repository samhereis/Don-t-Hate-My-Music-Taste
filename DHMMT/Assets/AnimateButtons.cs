using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
public class AnimateButtons : MonoBehaviour, IPointerExitHandler, IPointerClickHandler, IPointerEnterHandler
{
    [SerializeField] private float _onOverScale = 1.1f;
    [SerializeField] private float _normaleScale = 1;

    [Header("Timing")]
    [SerializeField] private float _animationDuration = 0.5f;
    [SerializeField] private float _delayBetweenAnimations = 0.1f;

    [Header("Events")]
    [SerializeField] private UnityEvent _onClick = new UnityEvent();

    public void OnPointerClick(PointerEventData eventData)
    {
        _onClick?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(_onOverScale, _animationDuration).SetEase(Ease.InOutBack);
    }

    public async void OnPointerExit(PointerEventData eventData)
    {
        await ExtentionMethods.Delay(_delayBetweenAnimations);

        transform.DOScale(_normaleScale, _animationDuration).SetEase(Ease.InOutBack);
    }
}