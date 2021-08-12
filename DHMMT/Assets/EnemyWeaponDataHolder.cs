using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponDataHolder : MonoBehaviour, WeaponDataHolder
{
    public GunData gunData { get; set; }
    public GunUse gunUse { get; set; }
    public GunAim gunAim { get; set; }

    private void Awake()
    {

    }

    public void Set(GunData gunData)
    {
        this.gunData = gunData;
    }
    public void Set(GunData gunData, GunUse gunUse)
    {
        this.gunData = gunData;
        this.gunUse = gunUse;
    }
    public void Set(GunData gunData, GunUse gunUse, GunAim gunAim)
    {
        this.gunData = gunData;
        this.gunUse = gunUse;
        this.gunAim = gunAim;
    }
}
