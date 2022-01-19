using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthData : MonoBehaviour
{
    // Handles enemie's health. The way of dying depends on match type. The way of dying script controlled by "Spawner.AddComponentToEnemy"
    
    public bool isAlive { get => _isAlive; set => _isAlive = value; }
    [SerializeField] private bool _isAlive;

    public float health { get => _health; set => _health = value; }
    [SerializeField] private float _health;

    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    [SerializeField] private float _maxHealth;

    [SerializeField] private List<Transform> _loots;

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health < 0 && isAlive == true)
        {
            isAlive = false;

            Die();
        };
    }

    private void Die()
    {

    }
}
