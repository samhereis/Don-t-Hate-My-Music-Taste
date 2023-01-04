using Photon.Pun;
using Pooling;
using UnityEngine;

namespace Gameplay.Bullets
{
    public class ProjectileBase : MonoBehaviourPunCallbacks
    {
        [SerializeField] protected BulletPooling_SO _pooling;
    }
}