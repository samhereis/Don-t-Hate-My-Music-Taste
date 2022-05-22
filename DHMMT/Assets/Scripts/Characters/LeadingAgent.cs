using UnityEngine;
using UnityEngine.AI;

namespace Characters
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class LeadingAgent : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Material _lightMaterial;
        [SerializeField] private Transform _target;

        [Header("Settings")]
        [SerializeField] private float _speed = 1.5f;

        private bool _isReachedTarget = false;

        private void OnEnable()
        {
            Move();
            _agent.speed = _speed;
        }

        private void OnDisable()
        {
            Stop();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Exit exit))
            {
                _isReachedTarget = true;
                Stop();
            }
        }

        private void FixedUpdate()
        {
            if (_agent.velocity.magnitude < 0.1f && _isReachedTarget == false) Move();
        }

        private void Move()
        {
            _agent.SetDestination(_target.position);
        }

        private void Stop()
        {
            _agent.isStopped = true;
            _agent.speed = 0;
        }

#if UNITY_EDITOR
        [ContextMenu("Setup")] public void Setup()
        {
            if (!_agent) _agent = GetComponent<NavMeshAgent>();
            if (!_target) _target = FindObjectOfType<Exit>().transform;
        }
#endif
    }
}