using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnExitRandomlyOnStart : MonoBehaviour
{
    public GameObject exit;

    [SerializeField] List<GameObject> spawns;

    private void OnEnable()
    {
        if (spawns.Count > 0)
        {
            exit.transform.position = spawns[Random.Range(0, spawns.Count)].transform.position;
        }
    }
}
