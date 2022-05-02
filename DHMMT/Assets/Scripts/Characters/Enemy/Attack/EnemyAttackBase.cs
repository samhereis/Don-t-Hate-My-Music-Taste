using Identifiers;
using UnityEngine;

namespace Characters.States
{
    public abstract class EnemyAttackBase : MonoBehaviour
    {
        [SerializeField] private IdentifierBase _target;

        public abstract bool CanAttack();
        public abstract void Attack();
    }
}
