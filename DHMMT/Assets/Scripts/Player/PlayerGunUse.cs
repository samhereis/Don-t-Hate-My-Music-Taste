using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGunUse : MonoBehaviour
{
    // Controlls the use of the gun that main players holds

    public static PlayerGunUse instance;

    public InteractableEquipWeapon DefaultWeapon;

    public InteractableEquipWeapon SecodWeapon;

    private void Awake()
    {
        ExtentionMethods.SetWithNullCheck(ref instance, this);
        DefaultWeapon.Interact(gameObject);
        FirstGunSlotUI.instance.Activate();
    }

    private void OnEnable()
    {
        PlayerInput.PlayersInputState.Gameplay.Fire.performed += Fire;
        PlayerInput.PlayersInputState.Gameplay.Fire.canceled  += Fire;

        PlayerInput.PlayersInputState.Gameplay.Reload.performed += Reload;

        PlayerInput.PlayersInputState.Gameplay.ChangeWeapon.performed += ChangeWeapon;

        PlayerInput.PlayersInputState.Gameplay.Sprint.performed += Sprint;
    }

    private void OnDisable ()
    {
        PlayerInput.PlayersInputState.Gameplay.Fire.performed -= Fire;
        PlayerInput.PlayersInputState.Gameplay.Fire.canceled  -= Fire;

        PlayerInput.PlayersInputState.Gameplay.Reload.performed -= Reload;

        PlayerInput.PlayersInputState.Gameplay.ChangeWeapon.performed -= ChangeWeapon;

        PlayerInput.PlayersInputState.Gameplay.Sprint.performed -= Sprint;
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
            else
            {
                MessageScript.instance.ShowMessage(MessageScript.instance.YouCanBuyGunsInTheShop, 3);
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
            else
            {
                MessageScript.instance.ShowMessage(MessageScript.instance.YouCanBuyGunsInTheShop, 3);
            }
        }
    }

    private void Sprint(InputAction.CallbackContext context)
    {
        PlayerWeaponDataHolder.instance.GunUseComponent.Use(false);
    }

    private void Fire(InputAction.CallbackContext context)
    {
        PlayerWeaponDataHolder.instance.GunUseComponent.Use(context.ReadValueAsButton());
    }

    private void Reload(InputAction.CallbackContext context)
    {
        StartCoroutine(PlayerWeaponDataHolder.instance.GunUseComponent?.Reload());
    }
}
