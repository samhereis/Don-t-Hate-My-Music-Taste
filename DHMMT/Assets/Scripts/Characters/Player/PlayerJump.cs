using Agents;
using Interfaces;
using Mirror;
using PlayerInputHolder;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class PlayerJump : NetworkBehaviour, IHasInput
    {
        private bool _canJump => _characterController.isGrounded == false || _doubleJump == true;

        [field: SerializeField] public Vector3 _playerVelocity { get; private set; }

        [Header("Components")]
        [SerializeField] private AnimationAgent _animator;
        [SerializeField] private CharacterController _characterController;

        [Header("Settings")]
        [SerializeField] private float _jumpHeight = 0.1f;
        [SerializeField] private float _gravityValue = -0.1f;
        [SerializeField] private bool _doubleJump;
        [SerializeField] private bool _doubleJumpable = true;

        [Header("SO")]
        [SerializeField] private Input_SO _inputContainer;
        private InputSettings _input => _inputContainer.input;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
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
            if (_canJump)
            {
                transform.localPosition += _playerVelocity;
                //_playerVelocity.y += _gravityValue * Time.deltaTime;
            }
        }

        private void Jump(InputAction.CallbackContext context)
        {
            if (_characterController.isGrounded)
            {
                _doubleJump = true;
                //_playerVelocity.y = _jumpHeight;
            }
            else if (_doubleJump && _characterController.isGrounded == false && _doubleJumpable == true)
            {
                //_playerVelocity.y += _jumpHeight * 1.25f;
                _doubleJump = false;
            }
        }

        public void PerformJump(float height)
        {
            //_playerVelocity.y = height;
        }

        public void EnableInput()
        {
            DisableInput();

            _input.Gameplay.Jump.performed += Jump;
        }

        public void DisableInput()
        {
            _input.Gameplay.Jump.performed -= Jump;
        }
    }
}