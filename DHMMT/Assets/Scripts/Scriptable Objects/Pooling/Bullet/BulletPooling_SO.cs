using Gameplay.Bullets;
using Samhereis.Pooling;
using UnityEngine;

namespace Pooling
{
    [CreateAssetMenu(fileName = "New Bullet Pooler", menuName = "Scriptables/Pooling Managers/Bullet Pooler")]
    public class BulletPooling_SO : PoolingManagerBase<ProjectileBase>
    {
        public virtual void Init(ProjectileBase projectile)
        {
            _poolable = projectile;
        }
    }
}