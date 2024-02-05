using ConstStrings;
using DataClasses;
using DependencyInjection;
using Identifiers;
using Interfaces;
using Observables;
using System;
using UnityEngine;

namespace Charatcers.Player
{
    public class PlayerHealth : MonoBehaviour, IDamagable, INeedDependencyInjection
    {
        public Action onDie { get; set; }

        [field: SerializeField, Header("Debug")] public float currentHealth { get; private set; } = 100;
        [field: SerializeField] public float maxHealth { get; private set; } = 100;
        [field: SerializeField] public bool isAlive { get; private set; } = true;
        [field: SerializeField] public IdentifierBase damagableIdentifier { get; private set; }

        [Inject(ObservableValue_ConstStrings.playerHealth)] private ObservableValue<PlayerHealthData> _playerHealthValue;

        private void Awake()
        {
            damagableIdentifier = GetComponent<IdentifierBase>();

            currentHealth = maxHealth;
            DependencyContext.diBox.InjectDataTo(this);
        }

        public void Die()
        {
            currentHealth = 0;
            isAlive = false;

            onDie?.Invoke();
        }

        public void TakeDamage(float damage, IDamagerWeapon damagerWeapon)
        {
            if (isAlive == true)
            {
                float healthBefore = currentHealth;

                currentHealth -= damage;

                if (currentHealth <= 0)
                {
                    Die();
                }

                _playerHealthValue?.ChangeValue(new PlayerHealthData(healthBefore, currentHealth, maxHealth));

                var aDamage = new PostDamageInfo();
                aDamage.damagerObject = damagerWeapon;
                aDamage.damagable = this;
                aDamage.damageAmount = damage;
                aDamage.healthAfterDamage = currentHealth;

                damagerWeapon.damagerActor.OnHasDamaged(aDamage);
            }
        }
    }
}