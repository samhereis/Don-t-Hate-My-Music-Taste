using Characters.States.Data;
using Helpers;
using Interfaces;
using Mirror;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay
{
    public class PlayerHealthData : HumanoidHealthBase, IDamagable //TODO: complete this class
    {
        [Command] 
        public override void TakeDamage(float damage)
        {
            Debug.Log(gameObject.name + " has been damaged for " + damage);

            health -= damage;

            if (health < 0)
            {

            }
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