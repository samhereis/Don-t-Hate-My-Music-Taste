using Identifiers;
using UnityEngine;

namespace Characters.States.Data
{
    [System.Serializable] public class EnemyMovementStateBase
    {
        [SerializeField] protected IdentifierBase _target;
        public IdentifierBase target => _target;

        [SerializeField] protected EnemyStates _enemy;

        public EnemyMovementStateBase(EnemyStates enemyStates)
        {
            _enemy = enemyStates;
        }

        public virtual void Move()
        {

        }
    }
}