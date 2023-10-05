using Identifiers;
using System;

namespace Interfaces
{
    public interface IDamagable
    {
        public Action onDie { get; set; }

        public IdentifierBase damagedObjectIdentifier { get; }
        public float currentHealth { get; }
        public float maxHealth { get; }
        public bool isAlive { get; }

        public void TakeDamage(float damage, IDamagerObject damager);
        public void Die();
    }
}