using DI;
using Helpers;
using Identifiers;
using System.Linq;
using UnityEngine;

namespace Characters.States.Data
{
    [System.Serializable]
    public class FoolowPlayer_EnemyMovementState : EnemyMovementStateBase
    {
        [SerializeField] private EnemyMovement _enemyMovement;
        [SerializeField] private bool _isNearPlayer = false;

        public FoolowPlayer_EnemyMovementState(EnemyStates enemy, EnemyMovement enemyMovement) : base(enemy)
        {
            _enemyMovement = enemyMovement;
            _target = Object.FindObjectsOfType<PlayerIdentifier>().ToList().GetRandomElement();
        }

        public override void Move()
        {
            if (_target == null) return;

            if (Vector3.Distance(_target.transform.position, _enemyMovement.transform.position) > _enemyMovement.currentDistanceToAttack) _isNearPlayer = false;
            else _isNearPlayer = true;

            if (_isNearPlayer == false) _enemyMovement.MoveTo(_target.transform); else _enemyMovement.Stop();
        }
    }
}