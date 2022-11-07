using Gameplay.Bullets;
using UnityEngine;

namespace Pooling
{
    [CreateAssetMenu(fileName = "New Bullet Pooler", menuName = "Scriptables/Pooling Managers/Bullet Pooler")]
    public class BulletPooling_SO : PoolerBase<ProjectileBase>
    {
        public virtual void Init(ProjectileBase projectile)
        {
            _poolable = projectile;
        }
    }
}