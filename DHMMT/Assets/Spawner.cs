using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    public GameObject playerPref;
    public GameObject enemyPref;

    public int numberOfEnemies;

    private void Start()
    {
        ExtentionMethods.SetWithNullCheck(ref instance, this);

        Spawn(playerPref,SpawnPoints.instance.GetRandomSpawn().position);
        SpawnEnemies();
    }
    private void OnEnable()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        yield return Wait.NewWait(5);

        int i = 0;
        while (i <= numberOfEnemies)
        {
            Spawn(enemyPref, SpawnPoints.instance.GetRandomSpawn().position);
            i++;
        }

        StopAllCoroutines();
    }

    private void OnDisable()
    {
        instance = null;
    }

    public void Spawn(GameObject obj)
    {
        Instantiate(obj);
    }

    public void Spawn(GameObject obj, Vector3 pos)
    {
        Instantiate(obj, pos, Quaternion.identity);
    }

    public void Spawn(GameObject obj, Transform pos)
    {
        Instantiate(obj, pos.position, Quaternion.identity);
    }

    public void SpawnEnemy(Transform pos)
    {
        Instantiate(enemyPref, pos.position, Quaternion.identity);
    }
}
