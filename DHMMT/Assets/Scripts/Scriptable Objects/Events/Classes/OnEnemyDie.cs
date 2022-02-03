using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using UnityEngine.Events;
using Helpers;
using Events;

namespace Identifiers
{
    [CreateAssetMenu(fileName = "On Enemy Die", menuName = "Scriptables/Events/On Enemy Die")]
    public class OnEnemyDie : EventWithOneParameterBase<EnemyIdentifier>
    {
        private void Awake()
        {
            
        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }

#if UNITY_EDITOR
        [ContextMenu("Setup")] public void Setup()
        {

        }
#endif
    }
}