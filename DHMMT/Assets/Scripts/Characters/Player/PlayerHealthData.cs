using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthData : MonoBehaviour,  IHealthData
{
    // Controlls main players health

    public static PlayerHealthData instance;

    public bool IsAlive { get => _isAlive; set => _isAlive = value; }
    [SerializeField] bool _isAlive = true;

    public float Health { get => _health; set { _health = value; HealthBar.instance.SetValue(_health); } }
    [SerializeField] float _health;

    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    [SerializeField] float _maxHealth;

    public List<checkPlayerInRange> numberOfCheckers;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(SetMaxHealth), 1, 0.5f);
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;

        if (Health < 0)
        {
            GetComponent<IMatchLoosable>().Loose();
        }
    }

    public IEnumerator TakeDamageContinuously(float time, float damage)
    {
        yield return Wait.NewWait(5);

        while (true)
        {
            if (numberOfCheckers.Count < 1)
            {
                TakeDamage(damage);
            }
            else
            {

            }

            yield return Wait.NewWait(time);
        }
    }

    private void SetMaxHealth()
    {
        if (HealthBar.instance == null || HealthBar.instance.SliderComponent == null || HealthBar.instance.SliderComponent.maxValue == MaxHealth)
        {
            return;
        }

        HealthBar.instance.SliderComponent.maxValue = MaxHealth;
        HealthBar.instance.SetValue(MaxHealth);
        HealthBar.instance.SetValue(_health);

        CancelInvoke(nameof(SetMaxHealth));
    }
}
