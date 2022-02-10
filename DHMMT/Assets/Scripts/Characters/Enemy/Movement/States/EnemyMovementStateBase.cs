using UnityEngine;

namespace Characters.States.Data
{
    public abstract class EnemyMovementStateBase
    {
        [SerializeField] protected EnemyStates _enemy;

        public EnemyMovementStateBase(EnemyStates enemyStates)
        {
            _enemy = enemyStates;
        }

        public abstract void Move();
    }
}