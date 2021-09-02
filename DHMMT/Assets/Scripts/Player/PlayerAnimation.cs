using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    int velocityHashY, velocityHashX;

    void Awake()
    {
        if(animator == null) animator = GetComponent<Animator>();

        velocityHashY = Animator.StringToHash("moveVelocityY");
        velocityHashX = Animator.StringToHash("moveVelocityX");
    }
    void OnEnable()
    {
        PlayerInput.input.Gameplay.Move.performed += SetAnimationValue;
        PlayerInput.input.Gameplay.Move.canceled  += SetAnimationValue;

        PlayerInput.input.Gameplay.Sprint.performed += SetSpeedMultiplier;
        PlayerInput.input.Gameplay.Sprint.canceled += SetSpeedMultiplier;

        PlayerInput.input.Gameplay.Aim.performed += Fire;
    }

    void OnDisable()
    {
        PlayerInput.input.Gameplay.Move.performed -= SetAnimationValue;
        PlayerInput.input.Gameplay.Move.canceled  -= SetAnimationValue;

        PlayerInput.input.Gameplay.Sprint.performed -= SetSpeedMultiplier;
        PlayerInput.input.Gameplay.Sprint.canceled  -= SetSpeedMultiplier;

        PlayerInput.input.Gameplay.Aim.performed -= Fire;
    }

    void SetAnimationValue(InputAction.CallbackContext context)
    {
        animator.SetFloat(velocityHashY, PlayerMove.instance.MoveInputValue.y);
        animator.SetFloat(velocityHashX, PlayerMove.instance.MoveInputValue.x);
    }

    void SetSpeedMultiplier(InputAction.CallbackContext context)
    {
        if (PlayerMove.instance.IsMoving == true)
        {
            animator.SetFloat(velocityHashY, 2);
            animator.SetFloat(velocityHashX, 2);
        }
    }

    void Fire(InputAction.CallbackContext context)
    {
        if (PlayerMove.instance.IsMoving == true)
        {
            animator.SetFloat(velocityHashY, 1);
            animator.SetFloat(velocityHashX, 1);
        }
    }
}
