using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    public GameObject playerPref;
    public GameObject enemyPref;

    [SerializeField] bool spawnOnStart = true;

    public int numberOfEnemies;

    private void Awake()
    {
        ExtentionMethods.SetWithNullCheck(ref instance, this);
    }

    private void Start()
    {
        if (numberOfEnemies > 0) StartCoroutine(SpawnEnemies());

        if (spawnOnStart == true)
        {
            Spawn(playerPref, SpawnPoints.instance.GetRandomSpawn().position);
        }
    }

    IEnumerator SpawnEnemies()
    {
        yield return Wait.NewWait(5);

        for (int i = 0;  i < numberOfEnemies; i++)
        {
            Spawn(enemyPref, SpawnPoints.instance.GetRandomSpawn().position);
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
    public void RepawnPlayer(GameObject caller,Transform pos)
    {
        if(PlayerHealthData.instance.Health < 0)
        {
            Destroy(caller);

            PlayerKillCount.instance.NullKillCount();

            HealthBar.instance.SetValue(HealthBar.instance.slider.maxValue);

            PlayerInput.input = new InputSettings();

            Instantiate(playerPref, pos.position, Quaternion.identity);
        }
    }
}
