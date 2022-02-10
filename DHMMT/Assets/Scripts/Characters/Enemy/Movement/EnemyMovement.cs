using Sripts;
using UnityEngine;
using UnityEngine.AI;

namespace Characters.States.Data
{
    public class EnemyMovement : HumanoidMovementStateData
    {
        [Header("Components")]
        [SerializeField] private EnemyStates _enemyStates;

        [Header("Settings")]
        [SerializeField] private float _minDistanceToPlayerToAttack = 5;
        [SerializeField] private float _maxDistanceToPlayerToAttack = 10;
        [SerializeField] private float _currentDistanceToPlayerToAttack;
        public float currentDistanceToAttack { get => _currentDistanceToPlayerToAttack; private set => _currentDistanceToPlayerToAttack = value; }

        [Header("Debug")]
        [SerializeField] private bool _isMoving;
        [SerializeField] private bool _isSprinting;

        public override bool isMoving => _isMoving;
        public override bool isSprinting => _isSprinting;

        private EnemyMovementStateBase _enemyMovementState;

        private void OnEnable()
        {
            _enemyMovementState = new FoolowPlayer_EnemyMovementState(_enemyStates, this);

            currentDistanceToAttack = Random.Range(_minDistanceToPlayerToAttack, _maxDistanceToPlayerToAttack);
        }

        public void MoveTo(Transform moveTo, int speed = 2)
        {
            MoveTo(moveTo.position);
        }

        public void MoveTo(Vector3 moveTo, int speed = 2)
        {
            if (_enemyStates.agent.isStopped == true) _enemyStates.agent.isStopped = false;

            if (_enemyStates.agent.destination != moveTo)
            {
                _enemyStates.agent.speed = speed;
                _enemyStates.agent.SetDestination(moveTo);

                _enemyStates.animator.SetFloat("moveVelocityY", _enemyStates.agent.speed);

                _isMoving = true;
            }
        }

        private void FixedUpdate()
        {
            _enemyMovementState?.Move();
        }

        public void Stop()
        {
            _enemyStates.agent.isStopped = true;

            _enemyStates.agent.speed = 0;

            _enemyStates.animator.SetFloat("moveVelocityY", _enemyStates.agent.speed);

            _enemyStates.agent.ResetPath();

            _isMoving = false;
        }
    }
}