using UnityEngine;

public class PlayerWeaponDataHolder : MonoBehaviour, WeaponDataHolder
{
    // Main player's weapons data

    public static PlayerWeaponDataHolder instance;

    public GunData DunDataCompoenent { get; set; }
    public GunUse GunUseComponent { get; set; }
    public GunAim GunAimComponent { get; set; }

    private void Awake()
    {
        ExtentionMethods.SetWithNullCheck(ref instance, GetComponent<PlayerWeaponDataHolder>());
    }

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
