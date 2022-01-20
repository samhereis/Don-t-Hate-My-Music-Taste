using Scriptables;
using Scriptables.Values;
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

    [SerializeField] private BoolValue_SO _isShooting;

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

    }

    public void ChangeWeapon(ScriptableGun.GunTypes slot)
    {

    }

    private void Sprint(InputAction.CallbackContext context)
    {

    }

    private void Fire(InputAction.CallbackContext context)
    {
        _isShooting.ChangeValue(context.ReadValueAsButton());

        _animator.SetFloat(_velocityHashY, 1);
        _animator.SetFloat(_velocityHashX, 1);
    }

    private void Reload(InputAction.CallbackContext context)
    {

    }
}
