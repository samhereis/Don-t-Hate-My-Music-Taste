using System.Collections;
using System.Collections.Generic;
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

    public List<checkPlayerInRange> numberOfCheckers;

    void Awake()
    {
        instance = this;
    }

    void OnEnable()
    {
        InvokeRepeating(nameof(SetMaxHealth), 1, 0.5f);
        StartCoroutine(TakeDamageContinuously(2, 20));
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;

        if (Health < 0)
        {
            GetComponent<IMatchController>().Loose();
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
