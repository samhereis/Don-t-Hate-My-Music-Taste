using Identifiers;
using Scriptables;
using Scriptables.Values;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Characters.States.Data
{
    public class PlayerGunUse : HumanoidAttackingStateData
    {
        // Controlls the use of the gun that main players holds

        [Header("Components")]
        [SerializeField] private Animator _animator;
        [SerializeField] private IdentifierBase _identifier;

        [Header("Guns")]
        [SerializeField] private InteractableEquipWeapon _defaultWeapon;

        [SerializeField] private InteractableEquipWeapon _secodWeapon;

        [Header("SO")]
        [SerializeField] private BoolValue_SO _isShooting;

        [SerializeField] private Input_SO _inputContainer;
        private InputSettings _input => _inputContainer.input;

        private int _velocityHashY, _velocityHashX;

        private void OnValidate()
        {
            if (_identifier == null) _identifier = GetComponent<IdentifierBase>();
        }

        private void Awake()
        {
            _velocityHashY = Animator.StringToHash("moveVelocityY");
            _velocityHashX = Animator.StringToHash("moveVelocityX");

            _defaultWeapon.Interact(_identifier);
        }

        private void OnEnable()
        {
            _input.Gameplay.Fire.performed += Fire;
            _input.Gameplay.Fire.canceled += Fire;

            _input.Gameplay.Reload.performed += Reload;

            _input.Gameplay.ChangeWeapon.performed += ChangeWeapon;

            _input.Gameplay.Sprint.performed += Sprint;
        }

        private void OnDisable()
        {
            _input.Gameplay.Fire.performed -= Fire;
            _input.Gameplay.Fire.canceled -= Fire;

            _input.Gameplay.Reload.performed -= Reload;

            _input.Gameplay.ChangeWeapon.performed -= ChangeWeapon;

            _input.Gameplay.Sprint.performed -= Sprint;
        }

        public void ChangeWeapon(InputAction.CallbackContext context)
        {

        }

        private void Sprint(InputAction.CallbackContext context)
        {

        }

        private void Fire(InputAction.CallbackContext context)
        {
            _isShooting.ChangeValue(context.ReadValueAsButton());

            onAttack?.Invoke(_isShooting.value);

            _animator.SetFloat(_velocityHashY, 1);
            _animator.SetFloat(_velocityHashX, 1);
        }

        private void Reload(InputAction.CallbackContext context)
        {

        }
    }
}