using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawnHolder : MonoBehaviour
{
    void Start()
    {
        SpawnPoints.instance.spawnPoints.Add(gameObject);
    }
}
