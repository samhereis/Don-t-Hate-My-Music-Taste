using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponDataHolder : MonoBehaviour, WeaponDataHolder
{
    // Data for enemie's weapon for fast reference on other scripts

    public GunData DunDataCompoenent { get; set; }
    public GunUse GunUseComponent { get; set; }
    public GunAim GunAimComponent { get; set; }

    public void Set(GunData gunData)
    {
        this.DunDataCompoenent = gunData;
    }
    public void Set(GunData gunData, GunUse gunUse)
    {
        this.DunDataCompoenent = gunData;
        this.GunUseComponent = gunUse;
    }
    public void Set(GunData gunData, GunUse gunUse, GunAim gunAim)
    {
        this.DunDataCompoenent = gunData;
        this.GunUseComponent = gunUse;
        this.GunAimComponent = gunAim;
    }
}
