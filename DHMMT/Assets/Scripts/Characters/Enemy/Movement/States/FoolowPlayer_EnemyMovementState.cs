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
            SetTarget();
        }

        public override void Move()
        {
            if (_target == null)
            {
                SetTarget();
                return;
            }

            if (Vector3.Distance(_target.transform.position, _enemyMovement.transform.position) > _enemyMovement.currentDistanceToAttack) _isNearPlayer = false;
            else _isNearPlayer = true;

            if (_isNearPlayer == false) _enemyMovement.MoveTo(_target.transform); else _enemyMovement.Stop();
        }

        private void SetTarget()
        {
            var player = Object.FindObjectsOfType<PlayerIdentifier>();

            if (player != null && player.Length > 0)
            {
                _target = player.ToList().GetRandom();
            }
        }
    }
}