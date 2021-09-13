using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponAim : MonoBehaviour
{
    // Controlls main player's weapon aim

    private PlayerWeaponDataHolder _weaponDataHolder;

    private Transform _weaponPosition;

    void OnEnable()
    {
        _weaponDataHolder = GetComponent<PlayerWeaponDataHolder>();
        _weaponPosition = GetComponent<HumanoidEquipWeaponData>().WeaponPosition;

        PlayerInput.PlayersInputState.Gameplay.Aim.performed += Aim;
        PlayerInput.PlayersInputState.Gameplay.Aim.canceled  += Aim;
    }

    void OnDisable()
    {
        PlayerInput.PlayersInputState.Gameplay.Aim.performed -= Aim;
        PlayerInput.PlayersInputState.Gameplay.Aim.canceled  -= Aim;
    }

    void Aim(InputAction.CallbackContext context)
    {
        if(context.ReadValueAsButton() == true)
        {
            _weaponDataHolder.GunAimComponent?.Aim(_weaponPosition, true);
            Crosshair.instance.SetActive(false);
        }
        else
        {
            _weaponDataHolder.GunAimComponent?.Aim(_weaponPosition, false);
            Crosshair.instance.SetActive(true);
        }
    }
}
