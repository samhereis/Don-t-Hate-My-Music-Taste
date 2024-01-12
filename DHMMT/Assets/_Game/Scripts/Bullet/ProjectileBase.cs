using Identifiers;
using Interfaces;
using Pooling;
using UnityEngine;

namespace Gameplay.Bullets
{
    [RequireComponent(typeof(BulletIdentifier))]
    public abstract class ProjectileBase : MonoBehaviour, IDamager
    {
        [SerializeField] protected BulletPooling_SO _pooling;

        public IDamagerActor damagerActor { get; private set; }
        public IdentifierBase damagerGameobject { get; private set; }

        private void Awake()
        {
            damagerGameobject = GetComponent<IdentifierBase>();
        }

        public virtual void Initialize(IDamagerActor damagerActor)
        {
            this.damagerActor = damagerActor;
        }

        public abstract void Damage(IDamagable damagable, float damage);
    }
}