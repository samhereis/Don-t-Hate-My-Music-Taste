using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnPoints : MonoBehaviour
{
    public static SpawnPoints instance;
    public List<GameObject> spawnPoints;

    public List<GameObject> BackUpspawnPoints;

    private void Awake()
    {
        ExtentionMethods.SetWithNullCheck(ref instance, this);
    }

    public Transform GetRandomSpawn()
    {
        if(spawnPoints.Count > 0)
        {
            return spawnPoints[Random.Range(0, spawnPoints.Count)].transform;
        }
        else
        {
            return BackUpspawnPoints[Random.Range(0, BackUpspawnPoints.Count)].transform;
        }
    }
}
