using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove instance;
    public float velocityY = 0.0f, velocityX = 0.0f; 
    public float speedMultiplier = 1, speed = 150;
    CharacterController characterController;
    Vector3 move;
    Vector2 MoveInputValue;
    void Awake()
    {
        instance = this;
        if (!characterController) characterController = GetComponent<CharacterController>(); 
    }
    void OnEnable()
    {

        PlayerInput.input.Gameplay.Move.performed += context => { MoveInputValue = context.ReadValue<Vector2>(); };
        PlayerInput.input.Gameplay.Move.canceled  += context => { MoveInputValue = Vector2.zero; };

        PlayerInput.input.Gameplay.Sprint.performed += context => { speedMultiplier = 3; };
        PlayerInput.input.Gameplay.Sprint.canceled  += context => { speedMultiplier = 1; };

        PlayerInput.input.Gameplay.Fire.performed += context => { speedMultiplier = 1; };
    }

    void FixedUpdate()
    {
        move = transform.right * MoveInputValue.x  + transform.forward * MoveInputValue.y;
        characterController.Move((speed * speedMultiplier) * Time.deltaTime * move);
    }
}
