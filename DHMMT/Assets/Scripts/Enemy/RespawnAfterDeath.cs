using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnAfterDeath : MonoBehaviour
{
    private void OnDestroy()
    {
        Spawner.instance.SpawnEnemy(SpawnPoints.instance.GetRandomSpawn().transform);
    }
}
