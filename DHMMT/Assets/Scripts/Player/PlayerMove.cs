using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove instance;
    public float velocityY = 0.0f, velocityX = 0.0f; 
    public float speedMultiplier = 1, speed = 150;
    CharacterController characterController;
    Vector3 move;
    public Vector2 MoveInputValue;
    public bool IsMoving = false;
    public float sprint;

    void Awake()
    {
        instance = this;
        if (!characterController) characterController = GetComponent<CharacterController>(); 
    }
    void OnEnable()
    {
        PlayerInput.input.Gameplay.Move.performed += Move;
        PlayerInput.input.Gameplay.Move.canceled  += Move;

        PlayerInput.input.Gameplay.Sprint.performed += Sprint;
        PlayerInput.input.Gameplay.Sprint.canceled  += Sprint;

        PlayerInput.input.Gameplay.Fire.performed += Fire;

        PlayerInput.input.Gameplay.Aim.performed += Fire;
    }

    void OnDisable()
    {
        PlayerInput.input.Gameplay.Move.performed -= Move;
        PlayerInput.input.Gameplay.Move.canceled  -= Move;

        PlayerInput.input.Gameplay.Sprint.performed -= Sprint;
        PlayerInput.input.Gameplay.Sprint.canceled  -= Sprint;

        PlayerInput.input.Gameplay.Fire.performed -= Fire;

        PlayerInput.input.Gameplay.Aim.performed -= Fire;
    }

    void Move(InputAction.CallbackContext context)
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

    void Sprint(InputAction.CallbackContext context)
    {
        sprint = context.ReadValue<float>();

        speedMultiplier = 1 + sprint * 2 ;
    }

    void Fire(InputAction.CallbackContext context)
    {
        speedMultiplier = 1;
    }

    void FixedUpdate()
    {
        move = transform.right * MoveInputValue.x  + transform.forward * MoveInputValue.y;
        characterController.Move((speed * speedMultiplier) * Time.deltaTime * move);
    }
}
