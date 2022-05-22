using Gameplay;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Gun", menuName = "Scriptable Object/Gun")]
    public class ScriptableGun : ScriptableObject
    {
        public enum GunTypes { Pistol, Rifle }
        [field: SerializeField] public GunTypes gunType { get; private set; }
        [field: SerializeField] public string gunName { get; private set; }
        [field: SerializeField] public Sprite gunIcon { get; private set; }
        [field: SerializeField] public GameObject gunPrefab { get; private set; }
        [field: SerializeField] public float gunDamage { get; private set; }
        [field: SerializeField] public int gunCost { get; private set; }
        [field: SerializeField] public InteractableEquipWeapon interact { get; private set; }
    }
}