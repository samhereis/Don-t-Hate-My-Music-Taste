using Scriptables;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace UI
{
    public class BackButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private UnityEvent _onClick = new UnityEvent();

        [SerializeField] private Input_SO _inputContainer;
        private InputSettings _input => _inputContainer.input;

        private void OnEnable()
        {
            _input.UI.Back.performed += Back;
        }

        private void OnDisable()
        {
            _input.UI.Back.performed -= Back;
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
}