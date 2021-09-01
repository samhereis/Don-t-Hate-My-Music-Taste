using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TS_D_OnEnemyDie : MonoBehaviour, IOnEnemyDie
{
    public void OnDie()
    {
        Spawner.instance.SpawnEnemy(SpawnPoints.instance.GetRandomSpawn().transform);

        PlayerKillCount.instance.IncreaseKillCount();

        SecondsCount.instance.IncreaseSeconds(5);

        Destroy(gameObject);
    }
}
