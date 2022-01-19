using Scriptables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGunUse : MonoBehaviour
{
    // Controlls the use of the gun that main players holds

    [SerializeField] private Animator _animator;

    [SerializeField] private InteractableEquipWeapon DefaultWeapon;

    [SerializeField] private InteractableEquipWeapon SecodWeapon;

    [SerializeField] private Input_SO _inputContainer;
    private InputSettings _input => _inputContainer.input;

    private int _velocityHashY, _velocityHashX;

    private void Awake()
    {
        _velocityHashY = Animator.StringToHash("moveVelocityY");
        _velocityHashX = Animator.StringToHash("moveVelocityX");

        DefaultWeapon.Interact(gameObject);
    }

    private void OnEnable()
    {
        _input.Gameplay.Fire.performed += Fire;
        _input.Gameplay.Fire.canceled  += Fire;

        _input.Gameplay.Reload.performed += Reload;

        _input.Gameplay.ChangeWeapon.performed += ChangeWeapon;

        _input.Gameplay.Sprint.performed += Sprint;
    }

    private void OnDisable ()
    {
        _input.Gameplay.Fire.performed -= Fire;
        _input.Gameplay.Fire.canceled  -= Fire;

        _input.Gameplay.Reload.performed -= Reload;

        _input.Gameplay.ChangeWeapon.performed -= ChangeWeapon;

        _input.Gameplay.Sprint.performed -= Sprint;
    }

    public void ChangeWeapon(InputAction.CallbackContext context)
    {
        if(DefaultWeapon.gameObject.activeSelf)
        {
            if (SecodWeapon != null)
            {
                SecodWeapon.Activate(true);
                SecodWeapon.Interact(gameObject);

                DefaultWeapon?.Activate(false);
            }
            else
            {

            }
        }
        else if(SecodWeapon.gameObject.activeSelf)
        {
            if (DefaultWeapon != null)
            {
                DefaultWeapon.Activate(true);
                DefaultWeapon.Interact(gameObject);

                SecodWeapon?.Activate(false);
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

                DefaultWeapon?.Activate(false);
            }
        }
        else if (slot == ScriptableGun.GunTypes.Rifle)
        {
            if (DefaultWeapon != null)
            {
                DefaultWeapon.Activate(true);
                DefaultWeapon.Interact(gameObject);

                SecodWeapon?.Activate(false);
            }
            else
            {

            }
        }
    }

    private void Sprint(InputAction.CallbackContext context)
    {

    }

    private void Fire(InputAction.CallbackContext context)
    {
        _animator.SetFloat(_velocityHashY, 1);
        _animator.SetFloat(_velocityHashX, 1);
    }

    private void Reload(InputAction.CallbackContext context)
    {

    }
}
