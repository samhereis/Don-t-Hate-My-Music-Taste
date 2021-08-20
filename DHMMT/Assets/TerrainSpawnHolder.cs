using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawnHolder : MonoBehaviour
{
    public GameObject Spawn;

    void Start()
    {
        SpawnPoints.instance.spawnPoints.Add(Spawn);
    }
}
