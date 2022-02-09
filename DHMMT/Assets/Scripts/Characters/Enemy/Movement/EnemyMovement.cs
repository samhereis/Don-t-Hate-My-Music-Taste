using Sripts;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : HumanoidMovementStateData
{
    [Header("Components")]
    [SerializeField] private EnemyStates _enemyStates;

    [Header("Debug")]
    [SerializeField] private bool _isMoving;
    [SerializeField] private bool _isSprinting;

    [SerializeField] private Vector3 _currentPath;

    public override bool isMoving => _isMoving;
    public override bool isSprinting => _isSprinting;

    private EnemyMovementStateBase _enemyMovementState;

    private void OnEnable()
    {
        _enemyStates.agent.ResetPath();
        _enemyMovementState = new FoolowPlayer_EnemyMovementState(_enemyStates, this); 
    }

    public void MoveTo(Transform moveTo, int speed = 2)
    {
        MoveTo(moveTo.position);
    }

    public void MoveTo(Vector3 moveTo, int speed = 2)
    {
        if (_enemyStates.agent.isStopped == false) _enemyStates.agent.isStopped = true;

        _currentPath = moveTo;

        if (_enemyStates.agent.destination != _currentPath)
        {
            _enemyStates.agent.speed = speed;
            _enemyStates.agent.SetDestination(_currentPath);

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

        _enemyStates.agent.ResetPath();

        _isMoving = false;
    }
}
