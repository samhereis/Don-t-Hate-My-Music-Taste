using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Scriptable Object/Gun")]
public class ScriptableGun : ScriptableObject
{
    // Plan for a gun for UI show

    public enum GunTypes { Pistol, Rifle }
    public GunTypes GunType;
    public string GunName { get => this.name; }
    public Sprite GunIcon;
    public GameObject GunPrefab;
    public float GunDamage { get => GunPrefab.GetComponent<GunUse>().Damage; }
    public int GunCost;
    public InteractableEquipWeapon Interact;
}
