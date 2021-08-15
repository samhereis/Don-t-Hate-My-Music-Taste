using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunUse : MonoBehaviour
{
    PlayerWeaponDataHolder weaponDataHolder;
    [SerializeField] InteractableEquipWeapon gun;
    bool IsShooting = false;

    void OnEnable()
    {
        weaponDataHolder = GetComponent<PlayerWeaponDataHolder>();

        gun.Interact(gameObject);

        PlayerInput.input.Gameplay.Fire.performed += (_) => IsShooting = true;
        PlayerInput.input.Gameplay.Fire.canceled += (_) => IsShooting = false;

        PlayerInput.input.Gameplay.Reload.performed += (_) => StartCoroutine(weaponDataHolder.gunUse?.Reload());
    }

    void FixedUpdate()
    {
        if(IsShooting)
        {
            weaponDataHolder.gunUse?.Shoot();
        }
    }
}
