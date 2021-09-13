using UnityEngine.Animations.Rigging;
using UnityEngine;

public class HumanoidEquipWeaponData : MonoBehaviour, IInteractorData
{
    // Equip weapon data of a humanoid

    public Transform RightSholder, WeaponAimPosition, WeaponPosition, AimTo, Cam, AllWeapons;
    public TwoBoneIKConstraint RightHandIK, LeftHandIK;
    public GameObject CurrentWeapon;
    public Animator AnimatorOfHumanoid;
    public RigBuilder RigBuilderOfHumanoid;

    public string InteractorName { get => transform.name; set { } }
}