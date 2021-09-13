using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    // Controlls player's move

    public static PlayerMove instance;

    public float SpeedMultiplier = 1, Speed = 150;

    private CharacterController _characterControllerComponent;

    private Vector3 move;

    public Vector2 MoveInputValue;

    public bool IsMoving = false;

    public float SprintValue;

    private void Awake()
    {
        instance = this;

        if (!_characterControllerComponent) _characterControllerComponent = GetComponent<CharacterController>(); 
    }

    private void OnEnable()
    {
        PlayerInput.PlayersInputState.Gameplay.Move.performed += Move;
        PlayerInput.PlayersInputState.Gameplay.Move.canceled  += Move;

        PlayerInput.PlayersInputState.Gameplay.Sprint.performed += Sprint;
        PlayerInput.PlayersInputState.Gameplay.Sprint.canceled  += Sprint;

        PlayerInput.PlayersInputState.Gameplay.Fire.performed += Fire;

        PlayerInput.PlayersInputState.Gameplay.Aim.performed += Fire;
    }

    private void OnDisable()
    {
        PlayerInput.PlayersInputState.Gameplay.Move.performed -= Move;
        PlayerInput.PlayersInputState.Gameplay.Move.canceled  -= Move;

        PlayerInput.PlayersInputState.Gameplay.Sprint.performed -= Sprint;
        PlayerInput.PlayersInputState.Gameplay.Sprint.canceled  -= Sprint;

        PlayerInput.PlayersInputState.Gameplay.Fire.performed -= Fire;

        PlayerInput.PlayersInputState.Gameplay.Aim.performed -= Fire;
    }

    private void FixedUpdate()
    {
        move = transform.right * MoveInputValue.x + transform.forward * MoveInputValue.y;
        _characterControllerComponent.Move((Speed * SpeedMultiplier) * Time.deltaTime * move);
    }

    private void Move(InputAction.CallbackContext context)
    {
        if (context.ReadValue<Vector2>() == Vector2.zero)
        {
            IsMoving = false;
        }
        else
        {
            IsMoving = true;
        }

        MoveInputValue = context.ReadValue<Vector2>();
    }

    private void Sprint(InputAction.CallbackContext context)
    {
        SprintValue = context.ReadValue<float>();

        SpeedMultiplier = 1 + SprintValue * 2 ;
    }

    private void Fire(InputAction.CallbackContext context)
    {
        SpeedMultiplier = 1;
    }
}
