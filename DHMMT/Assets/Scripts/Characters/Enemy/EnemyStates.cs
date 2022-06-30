using Samhereis.Agents;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Characters.States.Data
{
    public class EnemyStates : HumanoidData
    {
        [field: SerializeField] public NavMeshAgent agent { get; private set; }
        [field: SerializeField] public AnimationAgent animationAgent { get; private set; }

        [Header("Events")]
        public readonly UnityEvent<Collider> onTriggerEnter = new UnityEvent<Collider>();
        public readonly UnityEvent<Collider> onTriggerExit = new UnityEvent<Collider>();

        private void OnValidate()
        {
            Setup();
        }

        private void OnTriggerEnter(Collider other)
        {
            onTriggerEnter?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            onTriggerExit?.Invoke(other);
        }

        [ContextMenu(nameof(Setup))]
        public void Setup()
        {
            if (animationAgent == null) animationAgent = GetComponentInChildren<AnimationAgent>();
            if (agent == null) agent = GetComponent<NavMeshAgent>();
            if (humanoidMovementStateData == null) humanoidMovementStateData = GetComponent<EnemyMovement>();
        }
    }
}