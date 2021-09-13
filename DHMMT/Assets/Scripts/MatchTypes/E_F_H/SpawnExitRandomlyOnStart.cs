using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnExitRandomlyOnStart : MonoBehaviour
{
    // Randoly spawns Exit on "E-F-H" map

    public static SpawnExitRandomlyOnStart instance;

    private void Awake()
    {
        instance = this;
    }

    public List<GameObject> _spawns;

    private void OnEnable()
    {
        if (_spawns.Count > 0)
        {
            Vector3 loc = _spawns[Random.Range(0, _spawns.Count)].transform.position;

            transform.position = new Vector3(loc.x, 11, loc.z);
        }
    }
}
