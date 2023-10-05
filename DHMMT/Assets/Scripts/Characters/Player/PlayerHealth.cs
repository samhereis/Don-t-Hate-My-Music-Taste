using ConstStrings;
using DataClasses;
using DI;
using Identifiers;
using Interfaces;
using System;
using UnityEngine;
using Values;

namespace Charatcers.Player
{
    public class PlayerHealth : MonoBehaviour, IDamagable, IInitializable, IClearable, IDIDependent
    {
        public Action onDie { get; set; }

        [field: SerializeField, Header("Debug")] public float currentHealth { get; private set; } = 100;
        [field: SerializeField] public float maxHealth { get; private set; } = 100;
        [field: SerializeField] public bool isAlive { get; private set; } = true;
        [field: SerializeField] public IdentifierBase damagedObjectIdentifier { get; private set; }

        [Header("DI")]
        [DI(Event_DIStrings.playerHealth)][SerializeField] private ValueEvent<PlayerHealthData> _playerHealthValue;

        private void Awake()
        {
            damagedObjectIdentifier = GetComponent<IdentifierBase>();

            currentHealth = maxHealth;
        }

        private void Start()
        {
            (this as IDIDependent).LoadDependencies();
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

        }

        public void Clear()
        {

        }

        public void TakeDamage(float damage, IDamagerObject damagerObject)
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
                aDamage.damagerActor = damagerObject.damagerActor;
                aDamage.damagerObject = damagerObject;
                aDamage.damagable = this;
                aDamage.damageAmount = damage;
                aDamage.healthAfterDamage = currentHealth;

                damagerObject.damagerActor.OnDamaged(aDamage);
            }
        }

        public void Die()
        {
            currentHealth = 0;
            isAlive = false;

            onDie?.Invoke();
        }
    }
}