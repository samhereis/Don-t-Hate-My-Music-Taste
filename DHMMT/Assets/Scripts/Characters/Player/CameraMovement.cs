using PlayerInputHolder;
using Settings;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Camera
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Transform _moveCameraTowards;
        [SerializeField] private Transform _playerBody;

        [Header("SO")]
        [SerializeField] private FloatSetting_SO _sensitivity;
        [SerializeField] private Input_SO _inputContainer;
        private InputSettings _input => _inputContainer.input;

        [Header("Debug")]
        [SerializeField] private Vector2 _context;
        [SerializeField] private float _mouseX;
        [SerializeField] private float _mouseY;
        [SerializeField] private float _xRotation;

        private void Awake()
        {
            transform.position = _moveCameraTowards.position;
        }

        private void OnEnable()
        {
            _input.Gameplay.Look.performed += Look;
            _input.Gameplay.Look.canceled += Look;
        }

        private void OnDisable()
        {
            _input.Gameplay.Look.performed -= Look;
            _input.Gameplay.Look.canceled -= Look;
        }

        private void Look(InputAction.CallbackContext ctx)
        {
            _context = ctx.ReadValue<Vector2>() * _sensitivity.currentValue;

            _mouseX = _context.x;
            _mouseY = _context.y;

            transform.position = _moveCameraTowards.position;

            _xRotation -= _mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            _playerBody.Rotate(Vector3.up * _mouseX);
        }
    }
}