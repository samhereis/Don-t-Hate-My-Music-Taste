using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnExitRandomlyOnStart : MonoBehaviour
{
    // Randoly spawns Exit on "E-F-H" map

    public static SpawnExitRandomlyOnStart instance;

    [SerializeField] private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();

    private void Awake()
    {
        instance = this;
    }

    private async void OnEnable()
    {
        if (_spawnPoints.Count > 0)
        {
            await ExtentionMethods.Delay();

            Vector3 loc = _spawnPoints[Random.Range(0, _spawnPoints.Count)].transform.position;

            transform.position = new Vector3(loc.x, 11, loc.z);
        }
    }

    public void AddSpawnPoint(SpawnPoint sentSpanwPoint)
    {
        _spawnPoints.Add(sentSpanwPoint);
    }
}
