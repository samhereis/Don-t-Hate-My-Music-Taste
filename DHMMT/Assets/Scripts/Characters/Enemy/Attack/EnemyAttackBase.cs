using Identifiers;
using Samhereis;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.States.Attack
{
    public abstract class EnemyAttackBase : MonoBehaviour
    {
        [SerializeField] protected List<IdentifierBase> _targets;

        public abstract bool CanAttack();
        public abstract void Attack();
    }
}
