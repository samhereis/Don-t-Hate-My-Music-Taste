using Identifiers;
using Interfaces;
using PlayerInputHolder;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class SearchForInteractable : MonoBehaviour
    {
        private IInteractable _interactable;

        [SerializeField] private IdentifierBase _identifier;
        [SerializeField] private Input_SO _inputContainer;
        private InputSettings _input => _inputContainer.input;

        private void OnValidate()
        {
            if (_identifier == null) _identifier = GetComponent<IdentifierBase>();
        }

        private void OnEnable()
        {
            _input.Gameplay.Interact.performed += Interact;
        }

        private void OnDisable()
        {
            _input.Gameplay.Interact.performed -= Interact;
        }

        private void Interact(InputAction.CallbackContext context)
        {
            _interactable?.Interact(_identifier);
        }
    }
}