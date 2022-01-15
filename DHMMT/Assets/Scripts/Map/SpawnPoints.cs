using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnPoints : MonoBehaviour
{
    // Holds data of spawn points

    public static SpawnPoints instance;

    public List<GameObject> SpawnPointsList;
    public List<GameObject> PlayerSpawnPointList;

    private void Awake()
    {
        instance ??= this;
    }

    public Transform GetRandomSpawn()
    {
        if(SpawnPointsList.Count > 0)
        {
            return SpawnPointsList[Random.Range(0, SpawnPointsList.Count)].transform;
        }
        else
        {
            return PlayerSpawnPointList[Random.Range(0, PlayerSpawnPointList.Count)].transform;
        }
    }

    public Transform GetRandomSpawnForPlayer()
    {
        if (PlayerSpawnPointList.Count > 0)
        {
            return PlayerSpawnPointList[Random.Range(0, PlayerSpawnPointList.Count)].transform;
        }
        else
        {
            return SpawnPointsList[Random.Range(0, SpawnPointsList.Count)].transform;
        }
    }
}
