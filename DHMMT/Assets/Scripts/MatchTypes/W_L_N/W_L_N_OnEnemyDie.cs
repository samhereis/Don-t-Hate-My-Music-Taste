using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_L_N_OnEnemyDie : MonoBehaviour, IOnEnemyDie
{
    public void OnDie()
    {
        Spawner.instance.SpawnEnemy(SpawnPoints.instance.GetRandomSpawn().transform);

        PlayerKillCount.instance.IncreaseKillCount();

        Destroy(gameObject);
    }
}
