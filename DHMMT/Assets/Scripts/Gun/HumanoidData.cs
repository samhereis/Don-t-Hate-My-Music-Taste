using Interfaces;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Characters.States.Data
{
    [DisallowMultipleComponent]
    public class HumanoidData : MonoBehaviour, IInteractorData
    {
        public string InteractorName { get => transform.name; private set { } }

        [field: SerializeField] public HumanoidMovementStateData humanoidMovementStateData { get; protected set; }
        [field: SerializeField] public HumanoidAttackingStateData humanoidAttackingStateData { get; protected set; }
        [field: SerializeField] public TwoBoneIKConstraint RightHandIK { get; protected set; }
        [field: SerializeField] public TwoBoneIKConstraint LeftHandIK { get; protected set; }
        [field: SerializeField] public Transform weaponHolder { get; protected set; }
    }
}