using UnityEngine.Animations.Rigging;
using UnityEngine;
using Sripts;

[DisallowMultipleComponent]
public class HumanoidData : MonoBehaviour, IInteractorData
{
    [SerializeField] protected HumanoidMovementStateData _humanoidMovementStateData;
    public HumanoidMovementStateData humanoidMovementStateData => _humanoidMovementStateData;

    public TwoBoneIKConstraint RightHandIK;
    public TwoBoneIKConstraint LeftHandIK;

    [SerializeField] private Transform _weaponHolder;
    public Transform weapnHolder => _weaponHolder;

    public string InteractorName { get => transform.name; private set { } }
}