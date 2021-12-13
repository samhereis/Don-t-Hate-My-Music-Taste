using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    // Controlles players animations

    [SerializeField] private Animator _animator;

    private int _velocityHashY, _velocityHashX;

    private  void Awake()
    {
        if(_animator == null) _animator = GetComponent<Animator>();

        _velocityHashY = Animator.StringToHash("moveVelocityY");
        _velocityHashX = Animator.StringToHash("moveVelocityX");
    }

    private void OnEnable()
    {
        PlayerInput.PlayersInputState.Gameplay.Move.performed += SetAnimationValue;
        PlayerInput.PlayersInputState.Gameplay.Move.canceled  += SetAnimationValue;

        PlayerInput.PlayersInputState.Gameplay.Sprint.performed += SetSpeedMultiplier;
        PlayerInput.PlayersInputState.Gameplay.Sprint.canceled += SetSpeedMultiplier;

        PlayerInput.PlayersInputState.Gameplay.Aim.performed += Fire;
    }

    private void OnDisable()
    {
        PlayerInput.PlayersInputState.Gameplay.Move.performed -= SetAnimationValue;
        PlayerInput.PlayersInputState.Gameplay.Move.canceled  -= SetAnimationValue;

        PlayerInput.PlayersInputState.Gameplay.Sprint.performed -= SetSpeedMultiplier;
        PlayerInput.PlayersInputState.Gameplay.Sprint.canceled  -= SetSpeedMultiplier;

        PlayerInput.PlayersInputState.Gameplay.Aim.performed -= Fire;
    }

    private void SetAnimationValue(InputAction.CallbackContext context)
    {
        _animator?.SetFloat(_velocityHashY, PlayerMove.instance.MoveInputValue.y);
        _animator?.SetFloat(_velocityHashX, PlayerMove.instance.MoveInputValue.x);
    }

    private void SetSpeedMultiplier(InputAction.CallbackContext context)
    {
        if (PlayerMove.instance.IsMoving == true)
        {
            _animator.SetFloat(_velocityHashY, 2);
            _animator.SetFloat(_velocityHashX, 2);
        }
    }

    private void Fire(InputAction.CallbackContext context)
    {
        if (PlayerMove.instance.IsMoving == true)
        {
            _animator.SetFloat(_velocityHashY, 1);
            _animator.SetFloat(_velocityHashX, 1);
        }
    }
}
