using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Scriptable Object/Gun")]
public class ScriptableGun : ScriptableObject
{
    // Plan for a gun for UI show

    public enum GunTypes { Pistol, Rifle }
    public GunTypes gunType;
    public string gunName;
    public Sprite gunIcon;
    public GameObject gunPrefab;
    public float gunDamage;
    public int gunCost;
    public InteractableEquipWeapon interact;
}
