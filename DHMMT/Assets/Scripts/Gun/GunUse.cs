using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUse : MonoBehaviour
{
    public Animator WeaponAnimator;

    [Header("Sounds")]
    [SerializeField] AudioClip shootSound;
    [SerializeField] AudioSource audioSource;

    [Header("Shoot Capacity")]
    public float damage = 10;
    public float range = 100f;

    [Header("Ammo")]
    public int CurrentAmmo = 8;
    private int currentAmmo { get { return CurrentAmmo; } set { CurrentAmmo = value; if (CurrentAmmo <= 0) { canShoot = false; StartCoroutine(Reload()); } } }
    public int maxAmmo = 50;
    [SerializeField] float reloadTime;

    [Header("Shoot Timing")]
    public float fireRate = 0.2f;
    public float nextFire;

    [Header("Shoot States")]
    public bool isReloading = false;
    public bool canShoot = true;
    public bool IsShooting = false;

    void OnEnable()
    {
        audioSource.clip = shootSound;

        ExtentionMethods.SetWithNullCheck(ref audioSource, GetComponent<AudioSource>());
        ExtentionMethods.SetWithNullCheck(ref WeaponAnimator, GetComponent<GunData>().animator);
    }
    void OnDisable()
    {
        WeaponAnimator = null;
    }

    void FixedUpdate()
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
        if (Time.time > nextFire && canShoot)
        {
            nextFire = Time.time + fireRate;
            currentAmmo--;
            audioSource.Stop();
            audioSource.Play();

            WeaponAnimator.SetTrigger("Shoot");
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, range) && hit.collider.GetComponent<IHealthData>() != null)
            {
                hit.collider.GetComponent<IHealthData>().TakeDamage(damage);
            }
        }
    }
    public IEnumerator Reload()
    {
        while(currentAmmo < maxAmmo)
        {
            currentAmmo++;

            yield return Wait.NewWait(reloadTime);
        }
        if(currentAmmo >= maxAmmo)
        {
            canShoot = true;
            StopCoroutine(Reload());
        }
    }
}
