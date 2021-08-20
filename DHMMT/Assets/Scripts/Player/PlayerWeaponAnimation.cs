using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponAnimation : MonoBehaviour
{
    string SPEED = "Speed";

    bool sprint = false;

    bool move;

    private void OnEnable()
    {
        PlayerInput.input.Gameplay.Move.performed += Move;
        PlayerInput.input.Gameplay.Move.canceled += Move;

        PlayerInput.input.Gameplay.Sprint.performed += Sprint;
        PlayerInput.input.Gameplay.Sprint.canceled += Sprint;

        PlayerInput.input.Gameplay.Fire.performed += Fire; 
        
        PlayerInput.input.Gameplay.Aim.performed += Fire;
    }

    void Move(InputAction.CallbackContext context)
    {
        if (context.ReadValue<Vector2>() == Vector2.zero)
        {
            move = false;
        }
        else
        {
            move = true;
        }

        SetSpeed();
    }

    void Sprint(InputAction.CallbackContext context)
    {
        sprint = context.ReadValueAsButton();

        SetSpeed();
    }

    void Fire(InputAction.CallbackContext context)
    {
        sprint = false;

        SetSpeed();
    }

    void SetSpeed()
    {
        if(move == true && sprint == false)
        {
            PlayerWeaponDataHolder.instance.gunData?.animator.SetFloat(SPEED, 1);
        }
        else if(move == true && sprint == true)
        {
            PlayerWeaponDataHolder.instance.gunData?.animator.SetFloat(SPEED, 2);
        }
        else
        {
            PlayerWeaponDataHolder.instance.gunData?.animator.SetFloat(SPEED, 0);
        }
    }
}
