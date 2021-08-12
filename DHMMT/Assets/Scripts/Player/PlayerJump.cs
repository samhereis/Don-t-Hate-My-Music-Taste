using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    CharacterController characterController;
    public float jumpHeight = 0.1f, gravityValue = -0.9f; bool doubleJump;
    Vector3 playerVelocity;
    void Awake()
    {
        if(!characterController) characterController = GetComponent<CharacterController>(); 
    }
    void OnEnable()
    {
        PlayerInput.input.Gameplay.Jump.performed += context => Jump();
    }
    void FixedUpdate()
    {
        characterController.Move(playerVelocity);

        playerVelocity.y += gravityValue * Time.deltaTime;
    }
    void Jump()
    {
        if(characterController.isGrounded)
        {
            doubleJump = true;
            playerVelocity.y = jumpHeight;
        }
        else if(doubleJump)
        {
            playerVelocity.y += jumpHeight;
            doubleJump = false;
        }
    }
}
