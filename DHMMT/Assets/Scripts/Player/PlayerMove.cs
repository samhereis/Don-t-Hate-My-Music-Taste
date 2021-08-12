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
        if(!characterController) characterController = GetComponent<CharacterController>(); 
    }
    void OnEnable()
    {
        instance = this;
        
        PlayerInput.input.Gameplay.Move.performed += context => { this.MoveInputValue = context.ReadValue<Vector2>(); }; 
        PlayerInput.input.Gameplay.Move.canceled += context => { this.MoveInputValue = Vector2.zero; }; 

        PlayerInput.input.Gameplay.Sprint.performed += context => { speedMultiplier = 3; };
        PlayerInput.input.Gameplay.Sprint.canceled += context => { speedMultiplier = 1; };

        PlayerInput.input.Gameplay.Fire.performed += context => { speedMultiplier = 1; };
    }
    void OnDisable() { instance = null; }
    void FixedUpdate()
    {
        move = transform.right * MoveInputValue.x  + transform.forward * MoveInputValue.y;
        characterController.Move(move * (speed * speedMultiplier) * Time.deltaTime);
    }
}
