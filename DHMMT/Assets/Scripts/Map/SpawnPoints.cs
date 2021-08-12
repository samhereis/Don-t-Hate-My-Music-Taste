using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnPoints : MonoBehaviour
{
    public static SpawnPoints instance;
    public List<GameObject> spawnPoints;

    private void Awake()
    {
        ExtentionMethods.SetWithNullCheck(ref instance, this);
    }

    public Transform GetRandomSpawn()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Count)].transform;
    }
}
