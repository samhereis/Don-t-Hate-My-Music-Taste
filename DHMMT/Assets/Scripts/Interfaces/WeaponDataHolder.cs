using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface WeaponDataHolder
{
    // Bluepring of a humanoids weapon data holder

    public GunData DunDataCompoenent { get; set; }
    public GunUse GunUseComponent { get; set; }
    public GunAim GunAimComponent { get; set; }

    public void Set(GunData gunData);
    public void Set(GunData gunData, GunUse gunUse);
    public void Set(GunData gunData, GunUse gunUse, GunAim gunAim);
}
