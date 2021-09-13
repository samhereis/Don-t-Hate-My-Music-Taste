using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToSpawnNearObjects : MonoBehaviour
{
    // On "Escape from haters" map, if a player near a light add light's spawns to "SpawnPoints.instance.spawnPoints"

    [SerializeField] private bool _playerInRange;

    [SerializeField] private List<GameObject> _spawns;

    private void Awake()
    {
        foreach(GameObject obj in _spawns)
        {
            SpawnExitRandomlyOnStart.instance._spawns.Add(obj);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerGunUse>())
        {
            SpawnPoints.instance.SpawnPointsList = _spawns;
        }
    }

    private void OnTriggerExit(Collider other)
    {

    }
}
