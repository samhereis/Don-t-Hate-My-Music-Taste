using Identifiers;
using Interfaces;
using Pooling;
using UnityEngine;

namespace Gameplay.Bullets
{
    [RequireComponent(typeof(BulletIdentifier))]
    public abstract class ProjectileBase : MonoBehaviour, IDamagerObject
    {
        [SerializeField] protected BulletPooling_SO _pooling;

        public IDamagerActor damagerActor { get; private set; }
        public IdentifierBase damagerObjectIdentifier { get; private set; }

        private void Awake()
        {
            damagerObjectIdentifier = GetComponent<IdentifierBase>();
        }

        public virtual void Initialize(IDamagerActor damagerActor)
        {
            this.damagerActor = damagerActor;
        }

        public abstract void Damage(IDamagable damagable, float damage);
    }
}