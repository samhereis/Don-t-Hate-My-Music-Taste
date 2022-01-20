using Scriptables;
using Scriptables.Values;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Controlls player's move

    [SerializeField] private Animator _animator;

    public float SpeedMultiplier = 1, Speed = 150;

    private CharacterController _characterControllerComponent;

    private Vector3 move;

    public Vector2 MoveInputValue;

    [SerializeField] private BoolValue_SO _isMoving;

    [SerializeField] private BoolValue_SO _isSprinting;

    [SerializeField] private Input_SO _inputContainer;
    private InputSettings _input => _inputContainer.input;

    private int _velocityHashY, _velocityHashX;

    private void Awake()
    {
        _velocityHashY = Animator.StringToHash("moveVelocityY");
        _velocityHashX = Animator.StringToHash("moveVelocityX");

        if (!_characterControllerComponent) _characterControllerComponent = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        _input.Gameplay.Move.performed += Move;
        _input.Gameplay.Move.canceled += Move;

        _input.Gameplay.Sprint.performed += Sprint;
        _input.Gameplay.Sprint.canceled += Sprint;

        _input.Gameplay.Fire.performed += Fire;

        _input.Gameplay.Aim.performed += Fire;
    }

    private void OnDisable()
    {
        _input.Gameplay.Move.performed -= Move;
        _input.Gameplay.Move.canceled -= Move;

        _input.Gameplay.Sprint.performed -= Sprint;
        _input.Gameplay.Sprint.canceled -= Sprint;

        _input.Gameplay.Fire.performed -= Fire;

        _input.Gameplay.Aim.performed -= Fire;
    }

    private void FixedUpdate()
    {
        move = transform.right * MoveInputValue.x + transform.forward * MoveInputValue.y;
        _characterControllerComponent.Move((Speed * SpeedMultiplier) * Time.deltaTime * move);

        _animator?.SetFloat(_velocityHashY, MoveInputValue.y * SpeedMultiplier);
        _animator?.SetFloat(_velocityHashX, MoveInputValue.x * SpeedMultiplier);
    }

    private void Move(InputAction.CallbackContext context)
    {
        MoveInputValue = context.ReadValue<Vector2>();

        _isMoving.ChangeValue(MoveInputValue != Vector2.zero);
    }

    private void Sprint(InputAction.CallbackContext context)
    {
        SpeedMultiplier = 1 + (context.ReadValueAsButton() && _isMoving.value ? 1 : 0) * 2;

        _isSprinting.ChangeValue(SpeedMultiplier > 1);
    }

    private void Fire(InputAction.CallbackContext context)
    {
        SpeedMultiplier = 1;
    }
}
