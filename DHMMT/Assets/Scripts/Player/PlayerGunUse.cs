using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunUse : MonoBehaviour
{
    PlayerWeaponDataHolder weaponDataHolder;
    bool IsShooting = false;

    private void OnEnable()
    {
        weaponDataHolder = GetComponent<PlayerWeaponDataHolder>();

        PlayerInput.input.Gameplay.Fire.performed += (_) => IsShooting = true;
        PlayerInput.input.Gameplay.Fire.canceled += (_) => IsShooting = false;
    }
    private void OnDisable()
    {
        weaponDataHolder = null;
    }
    private void FixedUpdate()
    {
        if(IsShooting)
        {
            Shoot();
        }
    }
    void Shoot()
    {
        weaponDataHolder.gunUse?.Shoot();
    }
}
