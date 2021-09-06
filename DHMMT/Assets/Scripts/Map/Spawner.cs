using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    public GameObject playerPref;
    public GameObject enemyPref;

    public Component AddComponentToPlayer;
    public Component AddComponentToEnemy;

    [SerializeField] float WaitBeforeSpawnEnemies = 10;
    public float WaitBeforeSpawnPlayer = 5;

    [SerializeField] bool spawnOnStart = true;

    public List<GameObject> enemies;
    public List<GameObject> enemiesReserve;

    public int numberOfEnemies;

    private void Awake()
    {
        ExtentionMethods.SetWithNullCheck(ref instance, this);

        if (spawnOnStart == true)
        {
            StartCoroutine(SpawnPlayer());
        }
    }

    private void Start()
    {
        if (numberOfEnemies > 0) SpawnEnemies(numberOfEnemies);
    }

    private void OnDisable()
    {
        instance = null;
    }

    public void SpawnEnemies(int number)
    {
        StartCoroutine(SpawnEnemiesCoroutines(number));
    }

    IEnumerator SpawnEnemiesCoroutines(int number)
    {
        yield return Wait.NewWait(WaitBeforeSpawnEnemies);

        for (int i = 0; i < number; i++)
        {
            SpawnEnemy(SpawnPoints.instance.GetRandomSpawn());
        }

        StopCoroutine(SpawnEnemiesCoroutines(number));
    }

    public void SpawnEnemy(Transform pos)
    {
        GameObject enemy = Instantiate(enemyPref);

        enemy.AddComponent(AddComponentToEnemy.GetType());

        if(enemies.Count < 11)
        {
            enemies.Add(enemy);
            enemy.transform.position = pos.position;
        }
        else
        {
            enemy.SetActive(false);
            enemiesReserve.Add(enemy);
        }
    }

    IEnumerator SpawnPlayer()
    {
        yield return Wait.NewWait(WaitBeforeSpawnPlayer);

        GameObject obj = Instantiate(playerPref, SpawnPoints.instance.GetRandomSpawnForPlayer().position, Quaternion.identity);

        if (AddComponentToPlayer != null)
        {
            obj.AddComponent(AddComponentToPlayer.GetType());
        }

        StopCoroutine(SpawnPlayer());
    }

    public void RepawnPlayer(GameObject caller,Transform pos)
    {
        if(PlayerHealthData.instance.Health < 0 || PlayerHealthData.instance == null)
        {
            Destroy(caller);

            HealthBar.instance.SetValue(HealthBar.instance.slider.maxValue);

            PlayerInput.input = new InputSettings();

            StartCoroutine(SpawnPlayer());
        }
    }

    public void RepawnPlayer(Transform pos)
    {
        if (PlayerHealthData.instance.Health < 0 || PlayerHealthData.instance == null)
        {

            HealthBar.instance.SetValue(HealthBar.instance.slider.maxValue);

            PlayerInput.input = new InputSettings();

            StartCoroutine(SpawnPlayer());
        }
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
}
