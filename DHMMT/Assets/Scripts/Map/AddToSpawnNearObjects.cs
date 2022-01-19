using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToSpawnNearObjects : MonoBehaviour
{
    // On "Escape from haters" map, if a player near a light add light's spawns to "SpawnPoints.instance.spawnPoints"

    [SerializeField] private bool _playerInRange;

    [SerializeField] private List<SpawnPoint> _spawnPoints;

    [SerializeField] private SpawnExitRandomlyOnStart _spawnExitRandomlyOnStart;

    [SerializeField] private SpawnPoints _spawnPointsHolder;

    private void OnValidate()
    {
        _spawnExitRandomlyOnStart ??= FindObjectOfType<SpawnExitRandomlyOnStart>(true);

        _spawnPointsHolder ??= FindObjectOfType<SpawnPoints>(true);

        _spawnPoints.RemoveNulls();
    }

    private void Awake()
    {
        foreach(SpawnPoint obj in _spawnPoints)
        {
            _spawnExitRandomlyOnStart.AddSpawnPoint(obj);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerGunUse>())
        {
            _spawnPointsHolder.UpdateSpawnPoints(_spawnPoints);
        }
    }

    private void OnTriggerExit(Collider other)
    {

    }
}
