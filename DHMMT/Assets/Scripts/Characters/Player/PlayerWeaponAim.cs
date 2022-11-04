using Interfaces;
using Mirror;
using PlayerInputHolder;
using UnityEngine;
using UnityEngine.InputSystem;
using Values;

namespace Characters
{
    public class PlayerWeaponAim : NetworkBehaviour, IHasInput
    {
        [SerializeField] private BoolValue_SO _isAiming;

        [SerializeField] private Input_SO _inputContainer;
        private InputSettings _input => _inputContainer.input; 
        
        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();

            EnableInput();
        }

        private void OnEnable()
        {
            if (isLocalPlayer) EnableInput();
        }

        private void OnDisable()
        {
            if (isLocalPlayer) EnableInput();
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