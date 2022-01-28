using Scriptables;
using Scriptables.Values;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponAim : MonoBehaviour
{
    [SerializeField] private BoolValue_SO _isMoving;

    [SerializeField] private Input_SO _inputContainer;
    private InputSettings _input => _inputContainer.input;

    private void OnEnable()
    {
        _input.Gameplay.Aim.performed += Aim;
        _input.Gameplay.Aim.canceled += Aim;
    }

    private void OnDisable()
    {
        _input.Gameplay.Aim.performed -= Aim;
        _input.Gameplay.Aim.canceled -= Aim;
    }

    private void Aim(InputAction.CallbackContext context)
    {
        _isMoving.ChangeValue(context.ReadValueAsButton());
    }
}
