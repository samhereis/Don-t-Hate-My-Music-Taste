using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthData : MonoBehaviour, IHealthData
{
    // Handles enemie's health. The way of dying depends on match type. The way of dying script controlled by "Spawner.AddComponentToEnemy"
    
    public bool IsAlive { get => _isAlive; set => _isAlive = value; }
    [SerializeField] bool _isAlive;

    public float Health { get => _health; set => _health = value; }
    [SerializeField] float _health;

    public float MaxHealth { get => _MaxHealth; set => _MaxHealth = value; }
    [SerializeField] float _MaxHealth;

    public List<Transform> Loots;

    private IOnEnemyDie _onEnemyDie;

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health < 0 && IsAlive == true)
        {
            IsAlive = false;

            Die();
        };
    }

    private void Die()
    {
        Instantiate(Loots[Random.Range(0, Loots.Count)], transform.position, Quaternion.identity);

        _onEnemyDie = GetComponent<IOnEnemyDie>();

        if (_onEnemyDie != null)
        {
            _onEnemyDie.OnDie();
        }
        else
        {
            Spawner.instance.SpawnEnemy(SpawnPoints.instance.GetRandomSpawn().transform);

            PlayerKillCount.instance.IncreaseKillCount();

            Destroy(gameObject);
        }
    }
}
