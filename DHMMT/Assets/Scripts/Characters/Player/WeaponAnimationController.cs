using Scriptables;
using Scriptables.Values;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private string _speed = "Speed";

    [SerializeField] private BoolValue_SO _isMoving;

    [SerializeField] private BoolValue_SO _sprint;

    [SerializeField] private InteractableEquipWeapon _interactableEquipWeapon;

    private void Awake()
    {
        _interactableEquipWeapon.onEquip.AddListener(RegisterEvents);
        _interactableEquipWeapon.onUnequip.AddListener(DegisterEvents);
    }

    private void RegisterEvents(HumanoidEquipWeaponData sentData)
    {
        _isMoving.AddListener(SetSpeed);

        _sprint.AddListener(SetSpeed);
    }

    private void DegisterEvents(HumanoidEquipWeaponData sentData)
    {
        _isMoving.RemoveListener(SetSpeed);

        _sprint.RemoveListener(SetSpeed);
    }

    public void SetSpeed(bool value)
    {
        if (_isMoving.value && _sprint.value == false)
        {
            _animator.SetFloat(_speed, 1);
        }
        else if (_isMoving.value == true && _sprint.value == true)
        {
            _animator.SetFloat(_speed, 2);
        }
        else
        {
            _animator.SetFloat(_speed, 0);
        }
    }
}
