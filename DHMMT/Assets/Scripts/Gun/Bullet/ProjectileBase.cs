using Mirror;
using Pooling;
using UnityEngine;

namespace Gameplay.Bullets
{
    public class ProjectileBase : NetworkBehaviour
    {
        [SerializeField] protected BulletPooling_SO _pooling;

        private void OnValidate()
        {
            _pooling.Init(this);
        }
    }
}