using Scriptables.Values;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUse : MonoBehaviour
{
    // Controlls shoot of a weapon

    [SerializeField] private Animator _weaponAnimator;

    [Header("Sounds")]
    [SerializeField] private AudioClip _shootSound;
    [SerializeField] private AudioSource _audioSource;

    [Header("Ammo")]
    [SerializeField] private int _currentAmmo = 8;
    [SerializeField] private int _maxAmmo = 50;
    [SerializeField] private float _reloadTime;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletPosition;

    [Header("Shoot Timing")]
    [SerializeField] private float _fireRate = 0.2f;
    [SerializeField] private float _nextFire;

    [Header("Shoot States")]
    [SerializeField] private bool _isReloading = false;
    [SerializeField] private bool _canShoot = true;
    [SerializeField] private bool _shoot = false;

    [Header("Events")]
    [SerializeField] private BoolValue_SO _isShooting;

    [Header("Components")]
    [SerializeField] private InteractableEquipWeapon _interactableEquipWeapon;

    private void Awake()
    {
        _audioSource ??= GetComponent<AudioSource>();
        _weaponAnimator ??= GetComponentInChildren<Animator>();

        _interactableEquipWeapon?.onEquip.AddListener(OnEquip);
        _interactableEquipWeapon?.onUnequip.AddListener(OnUnEquip);
    }

    private void OnEnable()
    {
        _audioSource.clip = _shootSound;
    }

    private void FixedUpdate()
    {
        if (_shoot)
        {
            Shoot();
        }
    }

    private void OnEquip(HumanoidEquipWeaponData sentData)
    {
        _isShooting.AddListener(SetShoot);
    }

    private void OnUnEquip(HumanoidEquipWeaponData sentData)
    {
        _isShooting.RemoveListener(SetShoot);
    }

    public void SetShoot(bool value)
    {
        _shoot = value;
    }

    private void Shoot()
    {
        if ((Time.time > _nextFire) && (_canShoot == true))
        {
            _nextFire = Time.time + _fireRate;
            DecreaseAmmo();

            Instantiate(_bullet, _bulletPosition.position, _bulletPosition.rotation);

            _audioSource.Stop();
            _audioSource.Play();

            _weaponAnimator.SetTrigger("Shoot");
        }
    }

    public void DecreaseAmmo(int value = 1)
    {
        _currentAmmo -= value;
    }

    public void IncreaseAmmo(int value = 1)
    {
        _currentAmmo += value;
    }

    public void IncreaseAmmoAmmoInPersentageRelativeToMaxAmmo(int value)
    {
        _currentAmmo = _maxAmmo * value;

        if(_currentAmmo > _maxAmmo)
        {
            _currentAmmo = _maxAmmo;
        }
    }

    public void Reload()
    {
        while(_currentAmmo < _maxAmmo)
        {
            IncreaseAmmo();
        }
        if(_currentAmmo >= _maxAmmo)
        {
            _canShoot = true;
        }
    }
}
