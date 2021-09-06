using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGunUse : MonoBehaviour
{
    public static PlayerGunUse instance;

    public InteractableEquipWeapon DefaultWeapon;

    public InteractableEquipWeapon SecodWeapon;

    void Awake()
    {
        ExtentionMethods.SetWithNullCheck(ref instance, this);
        DefaultWeapon.Interact(gameObject);
        FirstGunSlotUI.instance.Activate();
    }

    void OnEnable()
    {
        PlayerInput.input.Gameplay.Fire.performed += Fire;
        PlayerInput.input.Gameplay.Fire.canceled  += Fire;

        PlayerInput.input.Gameplay.Reload.performed += Reload;

        PlayerInput.input.Gameplay.ChangeWeapon.performed += ChangeWeapon;
    }

    void OnDisable ()
    {
        PlayerInput.input.Gameplay.Fire.performed -= Fire;
        PlayerInput.input.Gameplay.Fire.canceled  -= Fire;

        PlayerInput.input.Gameplay.Reload.performed -= Reload;

        PlayerInput.input.Gameplay.ChangeWeapon.performed -= ChangeWeapon;
    }

    public void ChangeWeapon(InputAction.CallbackContext context)
    {
        if(DefaultWeapon.gameObject.activeSelf)
        {
            if (SecodWeapon != null)
            {
                SecodWeapon.Activate(true);
                SecodWeapon.Interact(gameObject);

                SecondGunSlotUI.instance.Activate();

                DefaultWeapon?.Activate(false);

                PlayerWeaponAnimation.instance.SetSpeed();
            }
        }
        else if(SecodWeapon.gameObject.activeSelf)
        {
            if (DefaultWeapon != null)
            {
                DefaultWeapon.Activate(true);
                DefaultWeapon.Interact(gameObject);

                FirstGunSlotUI.instance.Activate();

                SecodWeapon?.Activate(false);

                PlayerWeaponAnimation.instance.SetSpeed();
            }
        }
    }

    public void ChangeWeapon(ScriptableGun.GunTypes slot)
    {
        if (slot == ScriptableGun.GunTypes.Pistol)
        {
            if (SecodWeapon != null)
            {
                SecodWeapon.Activate(true);
                SecodWeapon.Interact(gameObject);

                SecondGunSlotUI.instance.Activate();

                DefaultWeapon?.Activate(false);
            }
        }
        else if (slot == ScriptableGun.GunTypes.Rifle)
        {
            if (DefaultWeapon != null)
            {
                DefaultWeapon.Activate(true);
                DefaultWeapon.Interact(gameObject);

                FirstGunSlotUI.instance.Activate();

                SecodWeapon?.Activate(false);
            }
        }
    }

    void Fire(InputAction.CallbackContext context)
    {
        PlayerWeaponDataHolder.instance.gunUse.Use(context.ReadValueAsButton());
    }

    void Reload(InputAction.CallbackContext context)
    {
        StartCoroutine(PlayerWeaponDataHolder.instance.gunUse?.Reload());
    }
}
