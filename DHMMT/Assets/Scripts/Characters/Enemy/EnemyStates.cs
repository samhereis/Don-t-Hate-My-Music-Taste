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
        [Header("Unity Components")]
        [SerializeField] private Rigidbody _rigidbody;
        public Rigidbody rigidbodyComponent => _rigidbody;

        [SerializeField] private NavMeshAgent _agent;
        public NavMeshAgent agent => _agent;

        [SerializeField] private Animator _animator;
        public Animator animator => _animator;

        [Header("Events")]
        public readonly UnityEvent<Collider> onTriggerEnter = new UnityEvent<Collider>();
        public readonly UnityEvent<Collider> onTriggerExit = new UnityEvent<Collider>();

        private void Start()
        {
            _humanoidMovementStateData ??= GetComponent<EnemyMovement>();
        }

        private void OnTriggerEnter(Collider other)
        {
            onTriggerEnter?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            onTriggerExit?.Invoke(other);
        }

#if UNITY_EDITOR
    [ContextMenu(nameof(Setup))]
    public void Setup()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody>();
    }
#endif
    }
}