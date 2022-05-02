using Sripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Characters.States.Data
{
    public class EnemyStates : HumanoidData
    {
        [Header("Components")]

        [SerializeField] private NavMeshAgent _agent;
        public NavMeshAgent agent => _agent;

        [SerializeField] private AnimationAgent _animationAgent;
        public AnimationAgent animationAgent => _animationAgent;

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
            if(_animationAgent == null) _animationAgent = GetComponentInChildren<AnimationAgent>();
            if (_agent == null) _agent = GetComponent<NavMeshAgent>();

            if (_humanoidMovementStateData == null) _humanoidMovementStateData = GetComponent<EnemyMovement>();
        }
    }
}