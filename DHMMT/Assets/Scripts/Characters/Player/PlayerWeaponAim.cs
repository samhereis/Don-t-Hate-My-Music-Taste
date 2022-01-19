using Scriptables;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponAim : MonoBehaviour
{
    // Controlls main player's weapon aim

    private PlayerWeaponDataHolder _weaponDataHolder;

    private Transform _weaponPosition;

    [SerializeField] private Input_SO _inputContainer;
    private InputSettings _input => _inputContainer.input;

    void OnEnable()
    {
        _weaponDataHolder = GetComponent<PlayerWeaponDataHolder>();
        _weaponPosition = GetComponent<HumanoidEquipWeaponData>().WeaponPosition;

        _input.Gameplay.Aim.performed += Aim;
        _input.Gameplay.Aim.canceled += Aim;
    }

    void OnDisable()
    {
        _input.Gameplay.Aim.performed -= Aim;
        _input.Gameplay.Aim.canceled -= Aim;
    }

    void Aim(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton() == true)
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
