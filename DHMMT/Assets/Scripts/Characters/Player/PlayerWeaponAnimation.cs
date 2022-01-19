using Scriptables;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponAnimation : MonoBehaviour
{
    // Controlls main player's weapon animation
    private string _speed = "Speed";

    private bool _sprint = false;

    private bool _move;

    [SerializeField] private Input_SO _inputContainer;
    private InputSettings _input => _inputContainer.input;

    private void OnEnable()
    {
        _input.Gameplay.Move.performed += Move;
        _input.Gameplay.Move.canceled += Move;

        _input.Gameplay.Sprint.performed += Sprint;
        _input.Gameplay.Sprint.canceled += Sprint;

        _input.Gameplay.Fire.performed += Fire;

        _input.Gameplay.Aim.performed += Fire;
    }

    private void OnDisable()
    {
        _input.Gameplay.Move.performed -= Move;
        _input.Gameplay.Move.canceled -= Move;

        _input.Gameplay.Sprint.performed -= Sprint;
        _input.Gameplay.Sprint.canceled -= Sprint;

        _input.Gameplay.Fire.performed -= Fire;

        _input.Gameplay.Aim.performed -= Fire;
    }

    private void Move(InputAction.CallbackContext context)
    {
        if (context.ReadValue<Vector2>() == Vector2.zero)
        {
            _move = false;
        }
        else
        {
            _move = true;
        }

        SetSpeed();
    }

    private void Sprint(InputAction.CallbackContext context)
    {
        _sprint = context.ReadValueAsButton();

        SetSpeed();
    }

    private void Fire(InputAction.CallbackContext context)
    {
        _sprint = false;

        SetSpeed();
    }

    public void SetSpeed()
    {
        if (_move == true && _sprint == false)
        {
            PlayerWeaponDataHolder.instance.DunDataCompoenent?.animator.SetFloat(_speed, 1);
        }
        else if (_move == true && _sprint == true)
        {
            PlayerWeaponDataHolder.instance.DunDataCompoenent?.animator.SetFloat(_speed, 2);
        }
        else
        {
            PlayerWeaponDataHolder.instance.DunDataCompoenent?.animator.SetFloat(_speed, 0);
        }
    }
}
