using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunUse : MonoBehaviour
{
    public static PlayerGunUse instance;

    public InteractableEquipWeapon DefaultWeapon;

    public InteractableEquipWeapon SecodWeapon;

    void Awake()
    {
        ExtentionMethods.SetWithNullCheck(ref instance, this);
        DefaultWeapon.Interact(gameObject);
    }

    void OnEnable()
    {
        PlayerInput.input.Gameplay.Fire.performed += (_) => PlayerWeaponDataHolder.instance.gunUse.Use(true);
        PlayerInput.input.Gameplay.Fire.canceled += (_) => PlayerWeaponDataHolder.instance.gunUse.Use(false);

        PlayerInput.input.Gameplay.Reload.performed += (_) => StartCoroutine(PlayerWeaponDataHolder.instance.gunUse?.Reload());
    }

    void OnDisable ()
    {
        PlayerInput.input.Gameplay.Fire.performed -= (_) => PlayerWeaponDataHolder.instance.gunUse.Use(true);
        PlayerInput.input.Gameplay.Fire.canceled  -= (_) => PlayerWeaponDataHolder.instance.gunUse.Use(false);

        PlayerInput.input.Gameplay.Reload.performed -= (_) => StartCoroutine(PlayerWeaponDataHolder.instance.gunUse?.Reload());
    }
}
