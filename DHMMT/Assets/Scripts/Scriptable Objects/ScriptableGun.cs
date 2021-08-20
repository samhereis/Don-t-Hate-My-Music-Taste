using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Scriptable Object/Gun")]
public class ScriptableGun : ScriptableObject
{
    public enum GunTypes { Pistol, Rifle }
    public GunTypes gunType;
    public string gunName { get => this.name; }
    public Sprite gunIcon;
    public GameObject gunPrefab;
    public float gunDamage { get => gunPrefab.GetComponent<GunUse>().damage; }
    public int gunCost;
    public InteractableEquipWeapon interact;
}
