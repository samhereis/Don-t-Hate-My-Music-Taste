using Helpers;
using Identifiers;
using Interfaces;
using Network;
using Photon.Pun;
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
        private InputActions _input => _inputContainer.input;

        private void OnValidate()
        {
            if (_identifier == null)
            {
                _identifier = GetComponent<IdentifierBase>();
                this.TrySetDirty();
            }
        }

        private void OnEnable()
        {
            if (_identifier.TryGet<PhotonView>().IsMine)
            {
                _input.Gameplay.Interact.performed += Interact;
            }
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