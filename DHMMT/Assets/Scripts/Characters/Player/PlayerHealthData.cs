using Characters.States.Data;
using Helpers;
using Interfaces;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay
{
    public class PlayerHealthData : HumanoidHealthBase, IDamagable //TODO: complete this class
    {
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