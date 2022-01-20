using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Scriptables.Values;

public class GunAim : MonoBehaviour
{
    // Aims a weapon

    [SerializeField] private Vector3 _aimPosition;
    [SerializeField] private Vector3 _initialPosition;

    [SerializeField] private float _animationDuration = 0.05f;

    [SerializeField] private BoolValue_SO _aimed;

    [SerializeField] private InteractableEquipWeapon _interactableEquipWeapon;

    [SerializeField] private HumanoidEquipWeaponData _equipData;

    private void Awake()
    {
        _interactableEquipWeapon = GetComponent<InteractableEquipWeapon>();
        _interactableEquipWeapon.onEquip.AddListener(OnEquip);
        _interactableEquipWeapon.onUnequip.AddListener(OnUnequip);
    }

    private void OnEquip(HumanoidEquipWeaponData sentEquipData)
    {
        _equipData = sentEquipData;

        _equipData.WeaponPosition.localPosition = _initialPosition;

        _aimed.AddListener(Aim);
    }

    private void OnUnequip(HumanoidEquipWeaponData sentEquipData)
    {
        _equipData = null;

        _aimed.RemoveListener(Aim);
    }

    public void Aim(bool isAimed)
    {
        if(isAimed == false)
        {
            _equipData.weapnHolder.DOLocalMove(_initialPosition, _animationDuration);
        }
        else
        {
            _equipData.weapnHolder.DOLocalMove(_aimPosition, _animationDuration);
        }
    }
}
