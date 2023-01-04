using Helpers;
using Identifiers;
using Interfaces;
using Network;
using Photon.Pun;
using PlayerInputHolder;
using UnityEngine;
using UnityEngine.InputSystem;
using Values;

namespace Characters
{
    public class PlayerWeaponAim : MonoBehaviour, IHasInput
    {
        [SerializeField] private BoolValue_SO _isAiming;
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
            EnableInput();
        }

        private void OnDisable()
        {
            EnableInput();
        }

        private void Aim(InputAction.CallbackContext context)
        {
            var isAiming = context.ReadValueAsButton();

            _isAiming.ChangeValue(isAiming);
        }

        public void EnableInput()
        {
            if (_identifier.TryGet<PhotonView>().IsMine)
            {
                DisableInput();

                _input.Gameplay.Aim.performed += Aim;
                _input.Gameplay.Aim.canceled += Aim;
            }
        }

        public void DisableInput()
        {
            _input.Gameplay.Aim.performed -= Aim;
            _input.Gameplay.Aim.canceled -= Aim;
        }
    }
}