using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using UnityEngine.Events;
using Helpers;
using Identifiers;

namespace Sripts
{
    public class FoolowPlayer_EnemyMovementState : EnemyMovementStateBase
    {
        private EnemyMovement _enemyMovement;

        private bool _isNearPlayer = false;

        public FoolowPlayer_EnemyMovementState(EnemyStates enemy, EnemyMovement enemyMovement) : base(enemy)
        {
            _enemyMovement = enemyMovement;

            _enemy.onTriggerEnter.AddListener(OnTriggerEnter);
            _enemy.onTriggerExit.AddListener(OnTriggerExit);
        }

        public override void Move()
        {
            if(_isNearPlayer == false) _enemyMovement.MoveTo(PlayerIdentifier.instance.transform);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out PlayerIdentifier player))
            {
                _isNearPlayer = true;

                _enemyMovement.Stop();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.TryGetComponent(out PlayerIdentifier player))
            {
                _isNearPlayer = false;
            }
        }
    }
}