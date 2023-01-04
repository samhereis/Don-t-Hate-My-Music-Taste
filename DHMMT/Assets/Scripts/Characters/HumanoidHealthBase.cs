using Interfaces;
using Photon.Pun;
using UnityEngine;

namespace Characters.States.Data
{
    public abstract class HumanoidHealthBase : MonoBehaviourPunCallbacks, IDamagable
    {
        [field: SerializeField] public bool isAlive { get; protected set; } = true;

        [SerializeField] protected float health = 100;
        [SerializeField] protected float maxHealth = 100;

        public abstract void RPC_TakeDamage(float damage);

        public abstract void TakeDamage(float damage);
    }
}