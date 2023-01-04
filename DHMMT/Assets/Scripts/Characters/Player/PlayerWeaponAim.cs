using Interfaces;
using PlayerInputHolder;
using UnityEngine;
using UnityEngine.InputSystem;
using Values;

namespace Characters
{
    public class PlayerWeaponAim : MonoBehaviour, IHasInput
    {
        [SerializeField] private BoolValue_SO _isAiming;

        [SerializeField] private Input_SO _inputContainer;
        private InputActions _input => _inputContainer.input;

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
            DisableInput();

            _input.Gameplay.Aim.performed += Aim;
            _input.Gameplay.Aim.canceled += Aim;
        }

        public void DisableInput()
        {
            _input.Gameplay.Aim.performed -= Aim;
            _input.Gameplay.Aim.canceled -= Aim;
        }
    }
}