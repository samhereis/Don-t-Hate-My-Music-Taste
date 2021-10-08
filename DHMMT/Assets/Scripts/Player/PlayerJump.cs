using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    // Controlls main player's jump

    private CharacterController characterController;

    public float JumpHeight = 0.1f, GravityValue = -0.1f;

    private bool _doubleJump;
    public bool DoubleJumpable = true;

    private Vector3 _playerVelocity;

    private void Awake()
    {
        if(!characterController) characterController = GetComponent<CharacterController>(); 
    }

    private void OnEnable()
    {
        PlayerInput.PlayersInputState.Gameplay.Jump.performed += Jump;
    }

    private void OnDisable()
    {
        PlayerInput.PlayersInputState.Gameplay.Jump.performed -= Jump;
    }

    private void FixedUpdate()
    {
        if(characterController.isGrounded == false || _doubleJump == true)
        {
            characterController.Move(_playerVelocity);

            _playerVelocity.y += GravityValue * Time.deltaTime;
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if(characterController.isGrounded)
        {
            _doubleJump = true;
            _playerVelocity.y = JumpHeight;
        }
        else if(_doubleJump && characterController.isGrounded == false && DoubleJumpable == true)
        {
            _playerVelocity.y += JumpHeight * 1.25f;
            _doubleJump = false;
        }
    }

    public void PerformJump(float height)
    {
        _playerVelocity.y = height;
    }
}
