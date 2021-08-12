using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface WeaponDataHolder
{
    public GunData gunData { get; set; }
    public GunUse gunUse { get; set; }
    public GunAim gunAim { get; set; }

    public void Set(GunData gunData);
    public void Set(GunData gunData, GunUse gunUse);
    public void Set(GunData gunData, GunUse gunUse, GunAim gunAim);
}
