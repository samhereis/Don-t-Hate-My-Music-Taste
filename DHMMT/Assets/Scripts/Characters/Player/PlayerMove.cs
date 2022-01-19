using Scriptables;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    // Controlls player's move

    [SerializeField] private Animator _animator;

    public float SpeedMultiplier = 1, Speed = 150;

    private CharacterController _characterControllerComponent;

    private Vector3 move;

    public Vector2 MoveInputValue;

    public bool IsMoving = false;

    public float SprintValue;

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
    }

    private void Move(InputAction.CallbackContext context)
    {
        MoveInputValue = context.ReadValue<Vector2>();

        if (MoveInputValue == Vector2.zero)
        {
            IsMoving = false;
        }
        else
        {
            IsMoving = true;
        }

        _animator?.SetFloat(_velocityHashY, MoveInputValue.y);
        _animator?.SetFloat(_velocityHashX, MoveInputValue.x);
    }

    private void Sprint(InputAction.CallbackContext context)
    {
        SprintValue = context.ReadValue<float>();

        SpeedMultiplier = 1 + SprintValue * 2;

        if (IsMoving == true)
        {
            _animator.SetFloat(_velocityHashY, 2);
            _animator.SetFloat(_velocityHashX, 2);
        }
    }

    private void Fire(InputAction.CallbackContext context)
    {
        SpeedMultiplier = 1;
    }
}
