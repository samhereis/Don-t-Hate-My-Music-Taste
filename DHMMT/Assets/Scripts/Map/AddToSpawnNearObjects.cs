using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToSpawnNearObjects : MonoBehaviour
{
    [SerializeField] bool PlayerInRange;

    [SerializeField] List<GameObject> spawns;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerGunUse>())
        {
            SpawnPoints.instance.spawnPoints = spawns;
        }
    }

    void OnTriggerExit(Collider other)
    {

    }
}
