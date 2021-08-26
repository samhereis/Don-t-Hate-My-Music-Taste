using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponDataHolder : MonoBehaviour, WeaponDataHolder
{
    public static PlayerWeaponDataHolder instance;

    public GunData gunData { get; set; }
    public GunUse gunUse { get; set; }
    public GunAim gunAim { get; set; }

    void Awake()
    {
        ExtentionMethods.SetWithNullCheck(ref instance, GetComponent<PlayerWeaponDataHolder>());
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
