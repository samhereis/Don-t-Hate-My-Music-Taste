using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BackButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private UnityEvent _onClick = new UnityEvent();

    private void OnEnable()
    {
        PlayerInput.playersInputState.UI.Back.performed += Back;
    }

    private void OnDisable()
    {
        PlayerInput.playersInputState.UI.Back.performed -= Back;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _onClick?.Invoke();
    }

    private void Back(InputAction.CallbackContext context)
    {
        _onClick?.Invoke();
    }

}
