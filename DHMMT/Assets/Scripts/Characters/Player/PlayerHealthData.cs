using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthData : MonoBehaviour
{
    // Controlls main players health

    public static PlayerHealthData instance;

    public bool IsAlive { get => _isAlive; set => _isAlive = value; }
    [SerializeField] bool _isAlive = true;

    public float Health { get => _health; set { _health = value; HealthBar.instance.SetValue(_health); } }
    [SerializeField] float _health;

    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    [SerializeField] float _maxHealth;

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

        }
    }

    public IEnumerator TakeDamageContinuously(float time, float damage)
    {
        yield return new WaitForSeconds(time);
    }

    private void SetMaxHealth()
    {

    }
}
