using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawnHolder : MonoBehaviour
{
    // When a terrain with a spawnPoint instantiates, add its spawnPoint to "SpawnPints" class

    private void Start()
    {
        SpawnPoints.instance.SpawnPointsList.Add(gameObject);
    }
}
