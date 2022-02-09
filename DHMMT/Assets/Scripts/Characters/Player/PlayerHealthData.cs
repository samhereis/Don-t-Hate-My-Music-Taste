using Helpers;
using Interfaces;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerHealthData : MonoBehaviour, IDamagable
{
    [SerializeField] bool _isAlive = true;

    public float health { get => _health; private set { _health = value; HealthBar.instance.SetValue(_health); } }
    [SerializeField] float _health;

    [SerializeField] float _maxHealth;

    private void OnEnable()
    {
        InvokeRepeating(nameof(SetMaxHealth), 1, 0.5f);
    }

    public float TakeDamage(float damage)
    {
        health -= damage;

        if (health < 0)
        {

        }

        return health;
    }

    public async Task TakeDamageContinuously(float time, float damage)
    {
        await AsyncHelper.Delay();
    }

    private void SetMaxHealth()
    {

    }
}
