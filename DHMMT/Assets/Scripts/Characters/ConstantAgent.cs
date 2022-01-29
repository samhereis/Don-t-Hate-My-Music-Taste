using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Helpers;
using UnityEngine.AI;
using System.Threading;

namespace Characters
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class ConstantAgent : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Transform _target;

        [Header("Settings")]
        [SerializeField] private float _speed = 1.5f;

        private CancellationTokenSource _cancellationTokenSource;

        private void OnEnable()
        {
            Move(_cancellationTokenSource = new CancellationTokenSource());
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out Exit exit))
            {
                _cancellationTokenSource.Cancel();
            }
        }

        private async void Move(CancellationTokenSource cancellationTokenSource)
        {
            while(!cancellationTokenSource.IsCancellationRequested && gameObject)
            {
                await AsyncHelper.Delay(2);

                _agent.speed = _speed;
                _agent.SetDestination(_target.position);
            }

            _agent.isStopped = true;

            _agent.speed = 0;
        }

#if UNITY_EDITOR
        [ContextMenu("Setup")] public void Setup()
        {
            if(!_agent) _agent = GetComponent<NavMeshAgent>();

            if(!_target) _target = FindObjectOfType<Exit>().transform;
        }
#endif
    }
}