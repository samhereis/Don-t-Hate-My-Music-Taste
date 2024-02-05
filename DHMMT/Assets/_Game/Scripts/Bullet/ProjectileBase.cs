using Identifiers;
using Interfaces;
using Pooling;
using UnityEngine;

namespace Gameplay.Bullets
{
    [RequireComponent(typeof(BulletIdentifier))]
    public class ProjectileBase : MonoBehaviour, IDamagerWeapon
    {
        [SerializeField] protected BulletPooling_SO _pooling;

        public IDamagerActor damagerActor { get; private set; }

        public virtual void Initialize(IDamagerActor newDamagerActor)
        {
            damagerActor = newDamagerActor;
        }

        public virtual void Damage(IDamagable damagable, float damage)
        {

        }
    }
}