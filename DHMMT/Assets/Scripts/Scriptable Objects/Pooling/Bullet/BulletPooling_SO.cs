using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using UnityEngine.Events;
using Helpers;
using Gameplay.Bullets;

namespace Pooling
{
    [CreateAssetMenu(fileName = "New Bullet Pooler", menuName = "Scriptables/Pooling Managers/Bullet Pooler")]
    public class BulletPooling_SO : PoolingManagerBase<ProjectileBase>
    {
        public virtual async void Init(ProjectileBase projectile)
        {
            await AsyncHelper.Delay();
            _poolable = projectile;
        }
    }
}