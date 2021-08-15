using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponAim : MonoBehaviour
{
    PlayerWeaponDataHolder weaponDataHolder;
    Transform weaponPosition;

    void OnEnable()
    {
        weaponDataHolder = GetComponent<PlayerWeaponDataHolder>();
        weaponPosition = GetComponent<EquipWeaponData>().weaponPosition;

        PlayerInput.input.Gameplay.Aim.performed += (_) => weaponDataHolder.gunAim?.Aim(weaponPosition);
    }
}
