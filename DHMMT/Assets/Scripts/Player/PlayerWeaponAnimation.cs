using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponAnimation : MonoBehaviour
{
    // Controlls main player's weapon animation

    public static PlayerWeaponAnimation instance;

    private string _speed = "Speed";

    private bool _sprint = false;

    private bool _move;

    private void OnEnable()
    {
        instance = this;

        PlayerInput.playersInputState.Gameplay.Move.performed += Move;
        PlayerInput.playersInputState.Gameplay.Move.canceled += Move;

        PlayerInput.playersInputState.Gameplay.Sprint.performed += Sprint;
        PlayerInput.playersInputState.Gameplay.Sprint.canceled += Sprint;

        PlayerInput.playersInputState.Gameplay.Fire.performed += Fire; 
        
        PlayerInput.playersInputState.Gameplay.Aim.performed += Fire;
    }

    private void OnDisable()
    {
        instance = null;

        PlayerInput.playersInputState.Gameplay.Move.performed -= Move;
        PlayerInput.playersInputState.Gameplay.Move.canceled  -= Move;

        PlayerInput.playersInputState.Gameplay.Sprint.performed -= Sprint;
        PlayerInput.playersInputState.Gameplay.Sprint.canceled  -= Sprint;

        PlayerInput.playersInputState.Gameplay.Fire.performed -= Fire;

        PlayerInput.playersInputState.Gameplay.Aim.performed -= Fire;
    }

    private void Move(InputAction.CallbackContext context)
    {
        if (context.ReadValue<Vector2>() == Vector2.zero)
        {
            _move = false;
        }
        else
        {
            _move = true;
        }

        SetSpeed();
    }

    private void Sprint(InputAction.CallbackContext context)
    {
        _sprint = context.ReadValueAsButton();

        SetSpeed();
    }

    private void Fire(InputAction.CallbackContext context)
    {
        _sprint = false;

        SetSpeed();
    }

    public void SetSpeed()
    {
        if(_move == true && _sprint == false)
        {
            PlayerWeaponDataHolder.instance.DunDataCompoenent?.animator.SetFloat(_speed, 1);
        }
        else if(_move == true && _sprint == true)
        {
            PlayerWeaponDataHolder.instance.DunDataCompoenent?.animator.SetFloat(_speed, 2);
        }
        else
        {
            PlayerWeaponDataHolder.instance.DunDataCompoenent?.animator.SetFloat(_speed, 0);
        }
    }
}
