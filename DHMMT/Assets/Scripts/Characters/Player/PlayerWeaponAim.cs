using Scriptables;
using Scriptables.Values;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponAim : MonoBehaviour
{
    // Controlls main player's weapon aim

    [SerializeField] private BoolValue_SO _isMoving;

    [SerializeField] private Input_SO _inputContainer;
    private InputSettings _input => _inputContainer.input;

    void OnEnable()
    {
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
        _isMoving.ChangeValue(context.ReadValueAsButton());
    }
}
