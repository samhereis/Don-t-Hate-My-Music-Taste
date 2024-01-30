using DataClasses;
using IdentityCards;
using Interfaces;
using UnityEngine;

namespace Identifiers
{
    public class EnemyIdentifier : IdentifierBase, IDamagerActor
    {
        [field: SerializeField] public EnemyIdentityCard identityCard { get; private set; }

        [field: SerializeField] public IdentifierBase damagerIdentifier { get; private set; }

        private void Awake()
        {
            damagerIdentifier = this;
        }

        public void OnHasDamaged(PostDamageInfo aDamage)
        {
            Debug.Log($"Enemy {aDamage.damagerObject.damagerGameobject.gameObject.name} damaged " +
            $"{aDamage.damagable.damagableIdentifier.gameObject.name}" +
            $" with {aDamage.damagerObject.damagerGameobject.gameObject.name}");
        }
    }
}