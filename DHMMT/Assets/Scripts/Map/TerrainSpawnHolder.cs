using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnPoint))]
public class TerrainSpawnHolder : MonoBehaviour
{
    // When a terrain with a spawnPoint instantiates, add its spawnPoint to "SpawnPints" class

    [SerializeField] private SpawnPoint _spawnPoint;

    private void OnValidate()
    {
        _spawnPoint ??= GetComponent<SpawnPoint>();
    }

    private void Start()
    {
        SpawnPoints.instance._spawnPointsList.Add(GetComponent<SpawnPoint>());
    }
}
