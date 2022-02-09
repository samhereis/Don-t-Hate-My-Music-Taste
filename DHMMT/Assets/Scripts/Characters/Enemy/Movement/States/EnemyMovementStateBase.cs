using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using UnityEngine.Events;
using Helpers;

namespace Sripts
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