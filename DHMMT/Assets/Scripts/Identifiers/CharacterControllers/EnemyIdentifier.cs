using DataClasses;
using Identifiers;
using IdentityCards;
using Interfaces;
using UnityEngine;

namespace Charatcers.Enemy
{
    public class EnemyIdentifier : IdentifierBase, IDamagerActor
    {
        [field: SerializeField] public EnemyIdentityCard identityCard { get; private set; }

        [field: SerializeField] public IdentifierBase damagerIdentifier { get; private set; }

        private void Awake()
        {
            damagerIdentifier = this;
        }

        public void OnDamaged(ADamage aDamage)
        {
            //Debug.Log($"Enemy {aDamage.damagerActor.damagerIdentifier.gameObject.name} damaged " +
                //$"{aDamage.damagable.damagedObjectIdentifier.gameObject.name}" +
                //$" with {aDamage.damagerObject.damagerObjectIdentifier.gameObject.name}");
        }
    }
}