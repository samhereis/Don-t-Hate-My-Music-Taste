using Agents;
using Interfaces;
using Mirror;
using PlayerInputHolder;
using UnityEngine;
using UnityEngine.InputSystem;
using Values;

namespace Characters.States.Data
{
    public class PlayerMovement : HumanoidMovementStateData, IHasInput
    {
        [Header("Components")]
        [SerializeField] private AnimationAgent _animator;
        [SerializeField] private CharacterController _characterControllerComponent;

        [Header("Settings")]
        [SerializeField] private float _speedMultiplier = 1;
        [SerializeField] private float _speed = 3;

        [Header("SO")]
        [SerializeField] private BoolValue_SO _isMoving;
        [SerializeField] private BoolValue_SO _isSprinting;

        [SerializeField] private Input_SO _inputContainer;
        private InputSettings _input => _inputContainer.input;

        [Header("Debug")]
        [SerializeField] private Vector2 _moveInputValue;
        [SerializeField] private Vector3 _move;
        [SerializeField] private int _velocityHashY, _velocityHashX;
        [SerializeField] private float _currentSpeedMultiplier = 1;

        public override bool isMoving => _isMoving.value;
        public override bool isSprinting => _isSprinting.value;

        private void Awake()
        {
            _velocityHashY = Animator.StringToHash("moveVelocityY");
            _velocityHashX = Animator.StringToHash("moveVelocityX");

            if (_characterControllerComponent == null) _characterControllerComponent = GetComponent<CharacterController>();
        }

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();

            EnableInput();
        }

        private void OnEnable()
        {
            if(isLocalPlayer) EnableInput();
        }

        private void OnDisable()
        {
            if (isLocalPlayer) EnableInput();
        }

        private void FixedUpdate()
        {
            DoMove();
        }

        private void Move(InputAction.CallbackContext context)
        {
            _moveInputValue = context.ReadValue<Vector2>();

            _isMoving?.ChangeValue(_moveInputValue != Vector2.zero);
            onIsMovingChange?.Invoke(isMoving, _moveInputValue.magnitude);
        }

        private void Sprint(InputAction.CallbackContext context)
        {
            _currentSpeedMultiplier = 1 + (context.ReadValueAsButton() && _isMoving.value ? 1 : 0) * _speedMultiplier;

            _isSprinting?.ChangeValue(_currentSpeedMultiplier > 1);

            onIsSprintingChange?.Invoke(isSprinting);
            onIsMovingChange?.Invoke(isMoving, _moveInputValue.magnitude);
        }

        private void Fire(InputAction.CallbackContext context)
        {
            _currentSpeedMultiplier = 1;
        }

        [Command]
        private void DoMove()
        {
            _move = transform.right * _moveInputValue.x + transform.forward * _moveInputValue.y;
            _characterControllerComponent.Move((_speed * _currentSpeedMultiplier) * Time.deltaTime * _move);

            SetAnimation();
        }

        [ClientRpc]
        private void SetAnimation()
        {
            _animator?.animator.SetFloat(_velocityHashY, _moveInputValue.y * _currentSpeedMultiplier);
            _animator?.animator.SetFloat(_velocityHashX, _moveInputValue.x * _currentSpeedMultiplier);
        }

        public void EnableInput()
        {
            DisableInput();

            _input.Gameplay.Move.performed += Move;
            _input.Gameplay.Move.canceled += Move;

            _input.Gameplay.Sprint.performed += Sprint;
            _input.Gameplay.Sprint.canceled += Sprint;

            _input.Gameplay.Fire.performed += Fire;

            _input.Gameplay.Aim.performed += Fire;
        }

        public void DisableInput()
        {
            _input.Gameplay.Move.performed -= Move;
            _input.Gameplay.Move.canceled -= Move;

            _input.Gameplay.Sprint.performed -= Sprint;
            _input.Gameplay.Sprint.canceled -= Sprint;

            _input.Gameplay.Fire.performed -= Fire;

            _input.Gameplay.Aim.performed -= Fire;
        }
    }
}