using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthData : MonoBehaviour, IHealthData
{
    public bool IsAlive { get => _isAlive; set => _isAlive = value; }
    [SerializeField] bool _isAlive;

    public float Health { get => _health; set => _health = value; }
    [SerializeField] float _health;

    public float MaxHealth { get => _MaxHealth; set => _MaxHealth = value; }
    [SerializeField] float _MaxHealth;

    public List<Transform> loots;

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health < 0)
        {
            Instantiate(loots[Random.Range(0, loots.Count)], transform.position, Quaternion.identity);

            Spawner.instance.SpawnEnemy(SpawnPoints.instance.GetRandomSpawn().transform);

            PlayerKillCount.instance.IncreaseKillCount();

            Destroy(gameObject);
        };
    }
}
