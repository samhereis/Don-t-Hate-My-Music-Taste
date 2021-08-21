using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponAim : MonoBehaviour
{
    PlayerWeaponDataHolder weaponDataHolder;
    Transform weaponPosition;

    void OnEnable()
    {
        weaponDataHolder = GetComponent<PlayerWeaponDataHolder>();
        weaponPosition = GetComponent<EquipWeaponData>().weaponPosition;

        PlayerInput.input.Gameplay.Aim.performed += Aim;
        PlayerInput.input.Gameplay.Aim.canceled += Aim;
    }

    void Aim(InputAction.CallbackContext context)
    {
        if(context.ReadValueAsButton() == true)
        {
            weaponDataHolder.gunAim?.Aim(weaponPosition, true);
            Crosshair.instance.SetActive(false);
        }
        else
        {
            weaponDataHolder.gunAim?.Aim(weaponPosition, false);
            Crosshair.instance.SetActive(true);
        }
    }
}
