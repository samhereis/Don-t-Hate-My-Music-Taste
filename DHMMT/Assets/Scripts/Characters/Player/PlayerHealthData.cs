using Characters.States.Data;
using Helpers;
using Interfaces;
using Network;
using Photon.Pun;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay
{
    public class PlayerHealthData : HumanoidHealthBase, IDamagable //TODO: complete this class
    {
        [PunRPC]
        public override void RPC_TakeDamage(float damage)
        {
            TakeDamage(damage);
        }

        public override void TakeDamage(float damage)
        {
            if (isAlive == false) return;

            Debug.Log(gameObject.name + " has been damaged for " + damage);

            health -= damage;

            if (health < 0)
            {
                isAlive = false;

                if (photonView.IsMine)
                {
                    Die();
                }
            }
        }

        private void Die()
        {
            Debug.Log("Die");

            PlayerSpawner.instance.Die();
        }

        public async Task TakeDamageContinuously(float time, float damage)
        {
            await AsyncHelper.Delay();
        }

        private void SetMaxHealth()
        {

        }
    }
}