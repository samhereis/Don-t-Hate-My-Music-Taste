using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyStates : MonoBehaviour
{
    [SerializeField] private EnemyMovement _enemyMovement;

    [Header("Unity Components")]
    [SerializeField] private Rigidbody _rigidbody;
    public Rigidbody rigidbody => _rigidbody;

    [SerializeField] private NavMeshAgent _agent;
    public NavMeshAgent agent => _agent;

    [SerializeField] private Animator _animator;
    public Animator animator => _animator;

    [Header("Events")]
    public readonly UnityEvent<Collider> _onTriggerEnter = new UnityEvent<Collider>();
    public readonly UnityEvent<Collider> _onTriggerExit = new UnityEvent<Collider>();

    private void Start()
    {
        _enemyMovement ??= GetComponent<EnemyMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _onTriggerEnter?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        _onTriggerExit?.Invoke(other);
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
