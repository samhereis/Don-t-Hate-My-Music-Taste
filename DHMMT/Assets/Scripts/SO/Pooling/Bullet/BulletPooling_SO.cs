using System.Threading.Tasks;
using Gameplay.Bullets;
using Helpers;
using Photon.Pun;
using UnityEngine;

namespace Pooling
{
    [CreateAssetMenu(fileName = "New Bullet Pooler", menuName = "Scriptables/Pooling Managers/Bullet Pooler")]
    public class BulletPooling_SO : PoolerBase<ProjectileBase>
    {
        public override async Task SpawnAsync(int quantity = 5, Transform parent = null)
        {
            for (int i = 0; i < quantity; i++)
            {
                await AsyncHelper.Delay();
                PutIn(PhotonNetwork.Instantiate(poolable.name, Vector3.zero, Quaternion.identity).GetComponent<ProjectileBase>());
            }
        }
    }
}