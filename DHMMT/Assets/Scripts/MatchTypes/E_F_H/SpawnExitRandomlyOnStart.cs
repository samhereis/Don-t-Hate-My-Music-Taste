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
            Vector3 loc = spawns[Random.Range(0, spawns.Count)].transform.position;

            exit.transform.position = new Vector3(loc.x, 11, loc.z);
        }
    }
}
