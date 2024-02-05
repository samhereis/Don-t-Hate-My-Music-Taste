using DataClasses;
using Demo.Scripts.Runtime;
using Helpers;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Charatcers.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private FPSController _fpsController;
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private FPSData _fpsData;

        private IDamagerActor _damagerActor;

        private void Awake()
        {
            if (_fpsController == null) { _fpsController = GetComponent<FPSController>(); }

            foreach (var rigidBody in _animator.GetComponentsInChildren<Rigidbody>(true))
            {
                rigidBody.isKinematic = true;
            }

            _fpsController.Initialize();
        }

        public async void Shoot(Vector3 targetPosition)
        {
            transform.LookAt(targetPosition);

            _fpsData.isFirePressed = true;
            _fpsData.isFireReleased = false;
            await AsyncHelper.NextFrame();
            _fpsData.isFirePressed = false;
            _fpsData.isFireReleased = true;
        }

        public void Reload()
        {
            _fpsData.isFirePressed = false;
            _fpsData.isFireReleased = true;
            _fpsData.isReload = true;
        }

        private void Update()
        {
            _fpsData.moveYRaw = _navMeshAgent.velocity.magnitude;
        }

        public void Sprint(float speed)
        {

        }

        public void Stop(float speed)
        {

        }
    }
}