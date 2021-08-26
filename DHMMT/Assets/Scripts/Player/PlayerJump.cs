using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    CharacterController characterController;
    public float jumpHeight = 0.1f, gravityValue = -0.03f; bool doubleJump;
    Vector3 playerVelocity;
    void Awake()
    {
        if(!characterController) characterController = GetComponent<CharacterController>(); 
    }
    void OnEnable()
    {
        PlayerInput.input.Gameplay.Jump.performed += Jump;
    }

    void OnDisable()
    {
        PlayerInput.input.Gameplay.Jump.performed -= Jump;
    }

    void FixedUpdate()
    {
        if(characterController.isGrounded == false || doubleJump == true)
        {
            characterController.Move(playerVelocity);

            playerVelocity.y += gravityValue * Time.deltaTime;
        }
    }
    void Jump(InputAction.CallbackContext context)
    {
        if(characterController.isGrounded)
        {
            doubleJump = true;
            playerVelocity.y = jumpHeight;
        }
        else if(doubleJump && characterController.isGrounded == false)
        {
            playerVelocity.y += jumpHeight * 2;
            doubleJump = false;
        }
        else
        {
            doubleJump = false;
        }
    }
}
