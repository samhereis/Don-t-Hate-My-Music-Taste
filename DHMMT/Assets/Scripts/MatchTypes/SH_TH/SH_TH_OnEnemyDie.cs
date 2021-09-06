using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_TH_OnEnemyDie : MonoBehaviour, IOnEnemyDie
{
    void OnEnable()
    {
        
    }

    public void OnDie()
    {
        Spawner.instance.enemies.Remove(gameObject);

        if(Spawner.instance.enemiesReserve.Count > 0)
        {
            var obj = Spawner.instance.enemiesReserve[Random.Range(0, Spawner.instance.enemiesReserve.Count)];

            obj.SetActive(true);

            obj.transform.position = SpawnPoints.instance.GetRandomSpawn().position;

            Spawner.instance.enemiesReserve.Remove(obj);
        }

        if(Spawner.instance.enemies.Count + Spawner.instance.enemiesReserve.Count == 0)
        {
            SH_TH_Page.instance.OnWin();
        }

        PlayerKillCount.instance.KillCount = Spawner.instance.enemies.Count + Spawner.instance.enemiesReserve.Count;

        Destroy(gameObject);
    }
}
