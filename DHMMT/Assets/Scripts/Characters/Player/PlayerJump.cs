using Agents;
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
    public class PlayerJump : MonoBehaviour, IHasInput
    {
        private bool _isGrounded => _characterController.isGrounded;

        [Header("Components")]
        [SerializeField] private AnimationAgent _animator;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private IdentifierBase _identifier;

        [Header("Settings")]
        [SerializeField] private float _jumpHeight = 0.1f;
        [SerializeField] private float _gravityValue = -0.1f;
        [SerializeField] private bool _doubleJump;
        [SerializeField] private bool _doubleJumpable = true;

        [Header("SO")]
        [SerializeField] private Input_SO _inputContainer;
        private InputActions _input => _inputContainer.input;

        [Header("Debug")]
        [SerializeField] private bool _isJumping;
        [SerializeField] private Vector3 _playerVelocity;

        private void OnValidate()
        {
            if (_identifier == null)
            {
                _identifier = GetComponent<IdentifierBase>();
                this.TrySetDirty();
            }
        }

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            EnableInput();
        }

        private void OnEnable()
        {
            EnableInput();
        }

        private void OnDisable()
        {
            EnableInput();
        }

        private void FixedUpdate()
        {
            if (_identifier.TryGet<PhotonView>().IsMine)
            {
                _characterController.Move(_playerVelocity * Time.deltaTime);
                _playerVelocity.y += _gravityValue * Time.deltaTime;

                if (_isGrounded)
                {
                    _isJumping = false;
                    _playerVelocity = Vector3.zero;
                }
            }
        }

        private void Jump(InputAction.CallbackContext context)
        {
            if (_isJumping == false)
            {
                _isJumping = true;

                _doubleJump = true;
                _playerVelocity.y = _jumpHeight;
            }
            else if (_doubleJump && _isGrounded == false && _doubleJumpable == true)
            {
                _playerVelocity.y += _jumpHeight * 1.25f;
                _doubleJump = false;
            }
        }

        public void PerformJump(float height)
        {
            _isJumping = true;
            _playerVelocity.y = height;
        }

        public void EnableInput()
        {
            if (_identifier.TryGet<PhotonView>().IsMine)
            {
                DisableInput();

                _input.Gameplay.Jump.performed += Jump;
            }
        }

        public void DisableInput()
        {
            _input.Gameplay.Jump.performed -= Jump;
        }
    }
}