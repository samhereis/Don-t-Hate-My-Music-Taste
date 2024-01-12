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
    public class PlayerHealth : MonoBehaviour, IDamagable, IInitializable, IDisposable, INeedDependencyInjection
    {
        public Action onDie { get; set; }

        [field: SerializeField, Header("Debug")] public float currentHealth { get; private set; } = 100;
        [field: SerializeField] public float maxHealth { get; private set; } = 100;
        [field: SerializeField] public bool isAlive { get; private set; } = true;
        [field: SerializeField] public IdentifierBase damagedGameobject { get; private set; }

        [Header("DI")]
        [Inject(ObservableValue_ConstStrings.playerHealth)][SerializeField] private ObservableValue<PlayerHealthData> _playerHealthValue;

        private void Awake()
        {
            damagedGameobject = GetComponent<IdentifierBase>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                TakeDamage(10, null);
            }
        }

        public void Initialize()
        {
            currentHealth = maxHealth;
            DependencyContext.diBox.InjectDataTo(this);
        }

        public void Dispose()
        {

        }

        public void Die()
        {
            currentHealth = 0;
            isAlive = false;

            onDie?.Invoke();
        }

        public void TakeDamage(float damage, IDamager damager)
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

                var aDamage = new ADamage();
                aDamage.damagerObject = damager;
                aDamage.damagedObject = this;
                aDamage.damageAmount = damage;
                aDamage.healthAfterDamage = currentHealth;

                damager.damagerActor.OnHasDamaged(aDamage);
            }
        }
    }
}