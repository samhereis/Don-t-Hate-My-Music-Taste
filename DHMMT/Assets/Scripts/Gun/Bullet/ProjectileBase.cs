using Helpers;
using Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Bullets
{
    public class ProjectileBase : MonoBehaviour
    {
        [SerializeField] protected BulletPooling_SO _pooling;

        public virtual async void Init(BulletPooling_SO bulletPooling)
        {
            await AsyncHelper.Delay();
            _pooling = bulletPooling;
        }
    }
}