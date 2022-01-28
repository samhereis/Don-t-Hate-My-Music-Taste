using UnityEngine.Animations.Rigging;
using UnityEngine;

public class HumanoidEquipWeaponData : MonoBehaviour, IInteractorData
{
    //TODO refactor

    public Transform RightSholder, WeaponPosition, AimTo, Cam, AllWeapons;
    public TwoBoneIKConstraint RightHandIK, LeftHandIK;
    public GameObject CurrentWeapon;

    [SerializeField] private Animator _animatorOfHumanoid;

    [SerializeField] private RigBuilder _rigBuilderOfHumanoid;

    [SerializeField] private Transform _weaponHolder;
    public Transform weapnHolder => _weaponHolder;

    public string InteractorName { get => transform.name; private set { } }
}