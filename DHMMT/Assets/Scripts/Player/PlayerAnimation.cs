using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    int velocityHashY, velocityHashX;
    public Vector2 MoveInputValue;
    void Awake()
    {
        if(!animator) animator = GetComponent<Animator>();

        velocityHashY = Animator.StringToHash("moveVelocityY");
        velocityHashX = Animator.StringToHash("moveVelocityX");
    }
    void OnEnable()
    {
        PlayerInput.input.Gameplay.Move.performed += context => { MoveInputValue = context.ReadValue<Vector2>(); SetAnimationValue(MoveInputValue); };
        PlayerInput.input.Gameplay.Move.canceled += context =>  { MoveInputValue = context.ReadValue<Vector2>(); SetAnimationValue(MoveInputValue); };

        PlayerInput.input.Gameplay.Sprint.performed += context => { SetAnimationValue(MoveInputValue * 2); };
        PlayerInput.input.Gameplay.Sprint.canceled += context =>  { SetAnimationValue(MoveInputValue); };
    }
    void SetAnimationValue(Vector2 context)
    {
        animator.SetFloat(velocityHashY, context.y);
        animator.SetFloat(velocityHashX, context.x); 
    }
}
