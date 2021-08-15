using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    int velocityHashY, velocityHashX;
    public Vector2 MoveInputValue;
    int SpeedMultiplier = 1;
    void Awake()
    {
        if(!animator) animator = GetComponent<Animator>();

        velocityHashY = Animator.StringToHash("moveVelocityY");
        velocityHashX = Animator.StringToHash("moveVelocityX");
    }
    void OnEnable()
    {
        PlayerInput.input.Gameplay.Move.performed += context =>  { MoveInputValue = context.ReadValue<Vector2>(); SetAnimationValue(); };
        PlayerInput.input.Gameplay.Move.canceled  += context =>  { MoveInputValue = context.ReadValue<Vector2>(); SetAnimationValue(); };

        PlayerInput.input.Gameplay.Sprint.performed += context =>  { SpeedMultiplier = 2; SetAnimationValue(); };
        PlayerInput.input.Gameplay.Sprint.canceled  += context =>  { SpeedMultiplier = 1; SetAnimationValue(); };
    }

    void SetAnimationValue()
    {
        animator.SetFloat(velocityHashY, MoveInputValue.y * SpeedMultiplier);
    }
}
