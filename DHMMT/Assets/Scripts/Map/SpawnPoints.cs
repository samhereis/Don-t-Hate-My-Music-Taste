using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnPoints : MonoBehaviour
{
    // Holds data of spawn points

    public static SpawnPoints instance;

    public List<SpawnPoint> _spawnPointsList = new List<SpawnPoint>();
    public List<SpawnPoint> _playerSpawnPointList = new List<SpawnPoint>();

    private void Awake()
    {
        instance ??= this;
    }

    public async void UpdateSpawnPoints(List<SpawnPoint> sentSpawnPoints)
    {
        await ExtentionMethods.Delay();

        _spawnPointsList.Clear();

        _spawnPointsList.AddRange(sentSpawnPoints);
    }

    public Transform GetRandomSpawn()
    {
        if(_spawnPointsList.Count > 0)
        {
            return _spawnPointsList[Random.Range(0, _spawnPointsList.Count)].transform;
        }
        else
        {
            return _playerSpawnPointList[Random.Range(0, _playerSpawnPointList.Count)].transform;
        }
    }

    public Transform GetRandomSpawnForPlayer()
    {
        if (_playerSpawnPointList.Count > 0)
        {
            return _playerSpawnPointList[Random.Range(0, _playerSpawnPointList.Count)].transform;
        }
        else
        {
            return _spawnPointsList[Random.Range(0, _spawnPointsList.Count)].transform;
        }
    }
}
