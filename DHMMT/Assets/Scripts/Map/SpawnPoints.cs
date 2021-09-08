using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnPoints : MonoBehaviour
{
    public static SpawnPoints instance;
    public List<GameObject> spawnPoints;

    public List<GameObject> playerSpawn;

    private void Awake()
    {
        ExtentionMethods.SetWithNullCheck(ref instance, this);
    }

    public Transform GetRandomSpawn()
    {
        if(spawnPoints.Count >= 1)
        {
            return spawnPoints[Random.Range(0, spawnPoints.Count)].transform;
        }
        else
        {
            return playerSpawn[Random.Range(0, playerSpawn.Count)].transform;
        }
    }

    public Transform GetRandomSpawnForPlayer()
    {
        if (playerSpawn.Count > 0)
        {
            return playerSpawn[Random.Range(0, playerSpawn.Count)].transform;
        }
        else
        {
            return spawnPoints[Random.Range(0, spawnPoints.Count)].transform;
        }
    }
}
