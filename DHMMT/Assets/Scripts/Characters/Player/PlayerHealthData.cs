using Helpers;
using Interfaces;
using Samhereis.Helpers;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay
{
    public class PlayerHealthData : MonoBehaviour, IDamagable //TODO: complete this class
    {
        [SerializeField] private bool _isAlive = true;
        [SerializeField] private float _health;
        [SerializeField] private float _maxHealth;

        private void OnEnable()
        {
            InvokeRepeating(nameof(SetMaxHealth), 1, 0.5f);
        }

        public float TakeDamage(float damage)
        {
            _health -= damage;

            if (_health < 0)
            {

            }

            return _health;
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