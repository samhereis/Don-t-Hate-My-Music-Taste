using DI;
using PlayerInputHolder;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace UI.Interaction
{
    public class BackButton : MonoBehaviour, IPointerClickHandler, IDIDependent
    {
        [field: SerializeField] public UnityEvent onBack { get; private set; } = new UnityEvent();

        [DI(ConstStrings.DIStrings.inputHolder)][SerializeField] private Input_SO _inputContainer;

        public void SubscribeToEvents()
        {
            (this as IDIDependent).LoadDependencies();

            if (_inputContainer != null) { _inputContainer.input.UI.Back.performed += Back; }
        }

        public void UnsubscribeFromEvents()
        {
            if (_inputContainer != null) { _inputContainer.input.UI.Back.performed -= Back; }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnBack();
        }

        private void Back(InputAction.CallbackContext context)
        {
            OnBack();
        }

        public void OnBack()
        {
            onBack?.Invoke();
        }
    }
}