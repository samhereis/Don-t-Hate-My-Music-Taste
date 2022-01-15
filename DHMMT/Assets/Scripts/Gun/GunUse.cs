using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUse : MonoBehaviour
{
    // Controlls shoot of a weapon

    public Animator WeaponAnimator;

    [Header("Sounds")]
    [SerializeField] private AudioClip _shootSound;
    [SerializeField] private AudioSource _audioSource;

    [Header("Shoot Capacity")]
    public float Damage = 10;
    public float Range = 100f;

    [Header("Ammo")]
    [SerializeField] private int _currentAmmo = 8;
    public int CurrentAmmo { get { return _currentAmmo; } set { _currentAmmo = value; if (_currentAmmo <= 0) { CanShoot = false; StartCoroutine(Reload()); } } }
    public int MaxAmmo = 50;
    [SerializeField] private float _reloadTime;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletPosition;

    [Header("Shoot Timing")]
    public float FireRate = 0.2f;
    public float NextFire;

    [Header("Shoot States")]
    public bool IsReloading = false;
    public bool CanShoot = true;
    public bool IsShooting = false;

    private void OnEnable()
    {
        _audioSource.clip = _shootSound;

        _audioSource ??= GetComponent<AudioSource>();
        WeaponAnimator ??= GetComponent<GunData>().animator;
    }

    private void OnDisable()
    {
        WeaponAnimator = null;
    }

    private void FixedUpdate()
    {
        if (IsShooting)
        {
            Shoot();
        }
    }

    public void Use(bool use)
    {
        IsShooting = use;
    }

    public void Shoot()
    {
        if ((Time.time > NextFire) && (CanShoot == true))
        {
            NextFire = Time.time + FireRate;
            CurrentAmmo--;
            Instantiate(_bullet, _bulletPosition.position, _bulletPosition.rotation);
            _audioSource.Stop();
            _audioSource.Play();

            WeaponAnimator.SetTrigger("Shoot");
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Range) && hit.collider.GetComponent<IHealthData>() != null)
            {
                hit.collider.GetComponent<IHealthData>().TakeDamage(Damage);
            }
        }
    }

    public IEnumerator Reload()
    {
        while(_currentAmmo < MaxAmmo)
        {
            _currentAmmo++;

            yield return Wait.NewWait(_reloadTime);
        }
        if(_currentAmmo >= MaxAmmo)
        {
            CanShoot = true;
            StopCoroutine(Reload());
        }
    }
}
