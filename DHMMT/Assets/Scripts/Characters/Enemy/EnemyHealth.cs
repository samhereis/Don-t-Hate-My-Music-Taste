using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using UnityEngine.Events;
using Helpers;
using Identifiers;

namespace Characters.States.Data
{
    public class EnemyHealth : HumanoidHealthBase
    {
        [SerializeField] private OnEnemyDie _onEnemyDie;

        public override bool isAlive { get; protected set; }

        private void OnEnable()
        {
            health = maxHealth;
            isAlive = true; 
        }

        public override float TakeDamage(float damage)
        {
            if(isAlive)
            {
                health -= damage;

                if(health <= 0)
                {
                    isAlive = false;

                    _onEnemyDie?.Invoke(GetComponent<EnemyIdentifier>());

                    Destroy(gameObject, 1);
                }
            }

            return health;
        }
    }
}