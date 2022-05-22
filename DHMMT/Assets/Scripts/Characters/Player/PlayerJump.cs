using Scriptables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class PlayerJump : MonoBehaviour
    {
        private bool _canJump => _characterController.isGrounded == false || _doubleJump == true;

        [Header("Components")]
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _characterController;

        [Header("Settings")]
        [SerializeField] private float _jumpHeight = 0.1f;
        [SerializeField] private float _gravityValue = -0.1f;
        [SerializeField] private bool _doubleJump;
        [SerializeField] private bool _doubleJumpable = true;

        [Header("SO")]
        [SerializeField] private Input_SO _inputContainer;
        private InputSettings _input => _inputContainer.input;

        private Vector3 _playerVelocity;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void OnEnable()
        {
            _input.Gameplay.Jump.performed += Jump;
        }

        private void OnDisable()
        {
            _input.Gameplay.Jump.performed -= Jump;
        }

        private void FixedUpdate()
        {
            if (_canJump)
            {
                _characterController.Move(_playerVelocity);
                _playerVelocity.y += _gravityValue * Time.deltaTime;
            }
        }

        private void Jump(InputAction.CallbackContext context)
        {
            if (_characterController.isGrounded)
            {
                _doubleJump = true;
                _playerVelocity.y = _jumpHeight;
            }
            else if (_doubleJump && _characterController.isGrounded == false && _doubleJumpable == true)
            {
                _playerVelocity.y += _jumpHeight * 1.25f;
                _doubleJump = false;
            }
        }

        public void PerformJump(float height)
        {
            _playerVelocity.y = height;
        }
    }
}