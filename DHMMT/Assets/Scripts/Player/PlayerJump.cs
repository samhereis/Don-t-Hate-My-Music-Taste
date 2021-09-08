using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    private CharacterController characterController;

    public float jumpHeight = 0.1f, gravityValue = -0.1f;

    bool doubleJump;
    public bool doubleJumpable = true;

    private Vector3 playerVelocity;

    private void Awake()
    {
        if(!characterController) characterController = GetComponent<CharacterController>(); 
    }
    private void OnEnable()
    {
        PlayerInput.input.Gameplay.Jump.performed += Jump;
    }

    private void OnDisable()
    {
        PlayerInput.input.Gameplay.Jump.performed -= Jump;
    }

    private void FixedUpdate()
    {
        if(characterController.isGrounded == false || doubleJump == true)
        {
            characterController.Move(playerVelocity);

            playerVelocity.y += gravityValue * Time.deltaTime;
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if(characterController.isGrounded)
        {
            doubleJump = true;
            playerVelocity.y = jumpHeight;
        }
        else if(doubleJump && characterController.isGrounded == false && doubleJumpable == true)
        {
            playerVelocity.y += jumpHeight * 1.25f;
            doubleJump = false;
        }
    }
}
