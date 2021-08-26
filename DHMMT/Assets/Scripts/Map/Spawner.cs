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

    [SerializeField] float WaitBeforeSpawnEnemies = 10;
    [SerializeField] float WaitBeforeSpawnPlayer = 5;

    [SerializeField] bool spawnOnStart = true;

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
        if (numberOfEnemies > 0) StartCoroutine(SpawnEnemies());
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

    IEnumerator SpawnEnemies()
    {
        yield return Wait.NewWait(WaitBeforeSpawnEnemies);

        for (int i = 0; i < numberOfEnemies; i++)
        {
            Spawn(enemyPref, SpawnPoints.instance.GetRandomSpawn().position);
        }

        StopCoroutine(SpawnEnemies());
    }

    public void SpawnEnemy(Transform pos)
    {
        Instantiate(enemyPref, pos.position, Quaternion.identity);
    }

    IEnumerator SpawnPlayer()
    {
        yield return Wait.NewWait(WaitBeforeSpawnPlayer);

        GameObject obj = Instantiate(playerPref);

        obj.AddComponent(AddComponentToPlayer.GetType());

        obj.transform.position = SpawnPoints.instance.GetRandomSpawn().position;

        StopCoroutine(SpawnPlayer());
    }

    public void RepawnPlayer(GameObject caller,Transform pos)
    {
        if(PlayerHealthData.instance.Health < 0 || PlayerHealthData.instance == null)
        {
            Destroy(caller);

            PlayerKillCount.instance.NullKillCount();

            HealthBar.instance.SetValue(HealthBar.instance.slider.maxValue);

            PlayerInput.input = new InputSettings();

            StartCoroutine(SpawnPlayer());
        }
    }

    public void RepawnPlayer(Transform pos)
    {
        if (PlayerHealthData.instance.Health < 0 || PlayerHealthData.instance == null)
        {
            PlayerKillCount.instance.NullKillCount();

            HealthBar.instance.SetValue(HealthBar.instance.slider.maxValue);

            PlayerInput.input = new InputSettings();

            StartCoroutine(SpawnPlayer());
        }
    }
}
