using Identifiers;
using Samhereis.Events;
using UnityEngine;

namespace Characters.States.Data
{
    public class EnemyHealth : HumanoidHealthBase
    {
        [SerializeField] private OnEnemyDie _onEnemyDie;
        [SerializeField] private EnemyIdentifier _enemyIdentifier;

        public override bool isAlive { get; protected set; }

        private void OnValidate()
        {
            if (_enemyIdentifier == null) _enemyIdentifier = GetComponent<EnemyIdentifier>();
        }

        private void OnEnable()
        {
            health = maxHealth;
            isAlive = true;
        }

        public override float TakeDamage(float damage)
        {
            if (isAlive)
            {
                health -= damage;
                if (health <= 0) Die();
            }

            return health;
        }

        private void Die()
        {
            isAlive = false;
            Destroy(gameObject, 1);
            _onEnemyDie?.Invoke(_enemyIdentifier);
        }
    }
}