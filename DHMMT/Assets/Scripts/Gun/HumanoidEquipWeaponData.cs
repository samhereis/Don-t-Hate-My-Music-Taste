using UnityEngine.Animations.Rigging;
using UnityEngine;

public class HumanoidEquipWeaponData : MonoBehaviour, IInteractorData
{
    // Equip weapon data of a humanoid

    public Transform RightSholder, WeaponPosition, AimTo, Cam, AllWeapons;
    public TwoBoneIKConstraint RightHandIK, LeftHandIK;
    public GameObject CurrentWeapon;
    public Animator AnimatorOfHumanoid;
    public RigBuilder RigBuilderOfHumanoid;

    [SerializeField] private Transform _weaponHolder;
    public Transform weapnHolder => _weaponHolder;

    public string InteractorName { get => transform.name; set { } }
}