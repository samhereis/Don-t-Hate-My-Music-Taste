using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using UnityEngine.Events;
using Helpers;

namespace Sripts
{
    public class EnemyMovementStateBase
    {
        [SerializeField] private EnemyStates _enemy;

        EnemyMovementStateBase(EnemyStates enemyStates)
        {
            _enemy = enemyStates;
        }
    }
}