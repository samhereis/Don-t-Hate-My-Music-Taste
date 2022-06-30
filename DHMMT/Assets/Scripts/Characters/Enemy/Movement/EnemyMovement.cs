using UnityEngine;

namespace Characters.States.Data
{
    public class EnemyMovement : HumanoidMovementStateData
    {
        [Header("Components")]
        [SerializeField] private EnemyStates _enemyStates;

        [Header("Settings")]
        [SerializeField] private float _minDistanceToPlayer = 5;
        public float currentDistanceToAttack => _minDistanceToPlayer;

        [Header("Debug")]
        [SerializeField] private bool _isMoving;
        [SerializeField] private bool _isSprinting;
        [SerializeReference] private EnemyMovementStateBase _enemyMovementState;

        public override bool isMoving => _isMoving;
        public override bool isSprinting => _isSprinting;


        private void OnEnable()
        {
            _enemyMovementState = new FoolowPlayer_EnemyMovementState(_enemyStates, this);
        }

        public void MoveTo(Transform moveTo, int speed = 2)
        {
            MoveTo(moveTo.position);
        }

        public void MoveTo(Vector3 moveTo, int speed = 2)
        {
            if (_enemyStates.agent.destination != moveTo)
            {
                _enemyStates.agent.speed = speed;
                _enemyStates.agent.SetDestination(moveTo);
                _enemyStates.animationAgent.animator.SetFloat("moveVelocityY", _enemyStates.agent.speed);

                _isMoving = true;
            }
        }

        private void FixedUpdate()
        {
            _enemyMovementState?.Move();
        }

        public void Stop()
        {
            _enemyStates.agent.speed = 0;
            _enemyStates.animationAgent.animator.SetFloat("moveVelocityY", _enemyStates.agent.speed);
            _enemyStates.agent.ResetPath();

            _isMoving = false;
        }
    }
}