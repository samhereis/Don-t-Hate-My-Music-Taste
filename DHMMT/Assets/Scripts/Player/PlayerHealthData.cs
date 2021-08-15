using System.Collections;
using UnityEngine;

public class PlayerHealthData : MonoBehaviour,  IHealthData
{
    public static PlayerHealthData instance;

    public bool IsAlive { get => _isAlive; set => _isAlive = value; }
    [SerializeField] bool _isAlive = true;

    public float Health { get => _health; set { _health = value; HealthBar.instance.SetValue(_health); } }
    [SerializeField] float _health;

    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    [SerializeField] float _maxHealth;

    void Awake()
    {
        instance = this;
    }

    void OnEnable()
    {
        InvokeRepeating(nameof(SetMaxHealth), 1, 0.5f);
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;

        if (Health < 0)
        {
            Spawner.instance.RepawnPlayer(gameObject, SpawnPoints.instance.GetRandomSpawn());
        }
    }
    void SetMaxHealth()
    {
        if (HealthBar.instance == null || HealthBar.instance.slider == null || HealthBar.instance.slider.maxValue == MaxHealth)
        {
            return;
        }

        HealthBar.instance.slider.maxValue = MaxHealth;
        HealthBar.instance.SetValue(MaxHealth);
        HealthBar.instance.SetValue(_health);

        CancelInvoke(nameof(SetMaxHealth));
    }
}
