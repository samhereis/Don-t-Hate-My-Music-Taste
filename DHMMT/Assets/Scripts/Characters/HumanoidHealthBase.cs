using Interfaces;
using Mirror;
using UnityEngine;

namespace Characters.States.Data
{
    public abstract class HumanoidHealthBase : NetworkBehaviour, IDamagable
    {
        [field: SerializeField] public bool isAlive { get; protected set; }

        [SerializeField] protected float health;
        [SerializeField] protected float maxHealth = 100;

        public abstract void TakeDamage(float damage);
    }
}