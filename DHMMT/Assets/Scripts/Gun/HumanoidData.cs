using UnityEngine.Animations.Rigging;
using UnityEngine;
using Sripts;
using Characters.States.Data;

[DisallowMultipleComponent]
public class HumanoidData : MonoBehaviour, IInteractorData
{
    [Header("States")]
    [SerializeField] protected HumanoidMovementStateData _humanoidMovementStateData; 
    public HumanoidMovementStateData humanoidMovementStateData => _humanoidMovementStateData;

    [SerializeField] protected HumanoidAttackingStateData _humanoidAttackingStateData;
    public HumanoidAttackingStateData humanoidAttackingStateData => _humanoidAttackingStateData;

    [Header("Hands For Equipping")]
    public TwoBoneIKConstraint RightHandIK;
    public TwoBoneIKConstraint LeftHandIK;

    [Header("Other")]
    [SerializeField] private Transform _weaponHolder;
    public Transform weapnHolder => _weaponHolder;

    public string InteractorName { get => transform.name; private set { } }
}