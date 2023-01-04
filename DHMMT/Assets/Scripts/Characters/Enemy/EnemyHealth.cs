using Identifiers;
using Photon.Pun;
using Samhereis.Events;
using UnityEngine;

namespace Characters.States.Data
{
    public class EnemyHealth : HumanoidHealthBase
    {
        [SerializeField] private OnEnemyDie _onEnemyDie;
        [SerializeField] private EnemyIdentifier _enemyIdentifier;

        private void OnValidate()
        {
            if (_enemyIdentifier == null) _enemyIdentifier = GetComponent<EnemyIdentifier>();
        }

        public override void OnEnable()
        {
            base.OnEnable();

            health = maxHealth;
            isAlive = true;
        }

        private void Die()
        {
            if (photonView.IsMine)
            {
                isAlive = false;
                
                PhotonNetwork.Destroy(photonView);
                _onEnemyDie?.Invoke(_enemyIdentifier);
            }
        }

        public override void TakeDamage(float damage)
        {
            if (isAlive)
            {
                health -= damage;
                if (health <= 0) Die();
            }
        }

        [PunRPC]
        public override void RPC_TakeDamage(float damage)
        {
            TakeDamage(damage);
        }
    }
}