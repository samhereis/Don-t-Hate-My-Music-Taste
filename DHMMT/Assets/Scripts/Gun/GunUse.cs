using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUse : MonoBehaviour
{
    public Animator WeaponAnimator;

    public float damage = 10;
    public float range = 100f;

    public int CurrentAmmo = 8;
    private int currentAmmo { get { return CurrentAmmo; } set { CurrentAmmo = value; if (CurrentAmmo <= 0) { canShoot = false; StartCoroutine(Reload()); } } }
    public int maxAmmo = 50;

    public float fireRate = 0.2f;
    public float nextFire;

    public bool isReloading = false;
    public bool canShoot = true;
    public bool IsShooting = false;

    public GameObject obj;

    Ray ray;

    WaitForSecondsRealtime wait = new WaitForSecondsRealtime(1);

    void OnEnable()
    {
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

    void Shoot()
    {
        if (Time.time > nextFire && canShoot)
        {
            nextFire = Time.time + fireRate;
            currentAmmo--;
            
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
            yield return wait;
        }
        if(currentAmmo >= maxAmmo)
        {
            canShoot = true;
            StopCoroutine(Reload());
        }
    }
}
