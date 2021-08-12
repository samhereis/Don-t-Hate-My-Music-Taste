using UnityEngine.Animations.Rigging;
using UnityEngine;

public class EquipWeaponData : MonoBehaviour, IInteractorData
{
    public static EquipWeaponData instance;
    public Transform rightSholder, weaponAimPosition, weaponPosition, aimTo, cam, allWeapons;
    public TwoBoneIKConstraint rightHandIK, leftHandIK;
    public GameObject currentWeapon;
    public Animator animator;
    public RigBuilder rigBuilder;

    public string InteractorName { get => transform.name; set { } }

    void OnEnable()
    {
        instance = this;
    }
}
