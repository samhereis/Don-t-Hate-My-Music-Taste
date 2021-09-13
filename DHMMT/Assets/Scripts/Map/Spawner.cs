using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Controlls object spawning

    public static Spawner instance;

    public GameObject PlayerPref;
    public GameObject EnemyPref;

    public Component AddComponentToPlayer;
    public Component AddComponentToEnemy;

    [SerializeField] private float _waitBeforeSpawnEnemies = 10;
    public float WaitBeforeSpawnPlayer = 5;

    [SerializeField] private bool _spawnOnStart = true;

    public List<GameObject> Enemies;
    public List<GameObject> EnemiesReserve;

    public int NumberOfEnemies;

    private void Awake()
    {
        ExtentionMethods.SetWithNullCheck(ref instance, this);

        if (_spawnOnStart == true)
        {
            StartCoroutine(SpawnPlayer());
        }
    }

    private void Start()
    {
        if (NumberOfEnemies > 0) SpawnEnemies(NumberOfEnemies);
    }

    private void OnDisable()
    {
        instance = null;
    }

    public void SpawnEnemies(int number)
    {
        StartCoroutine(SpawnEnemiesCoroutines(number));
    }

    private IEnumerator SpawnEnemiesCoroutines(int number)
    {
        yield return Wait.NewWait(_waitBeforeSpawnEnemies);

        for (int i = 0; i < number; i++)
        {
            SpawnEnemy(SpawnPoints.instance.GetRandomSpawn());
        }

        StopCoroutine(SpawnEnemiesCoroutines(number));
    }

    public void SpawnEnemy(Transform pos)
    {
        GameObject enemy = Instantiate(EnemyPref, SpawnPoints.instance.GetRandomSpawn().position, Quaternion.identity);

        enemy.AddComponent(AddComponentToEnemy.GetType());

        if(Enemies.Count < 11)
        {
            Enemies.Add(enemy);
        }
        else
        {
            enemy.SetActive(false);
            EnemiesReserve.Add(enemy);
        }
    }

    private IEnumerator SpawnPlayer()
    {
        yield return Wait.NewWait(WaitBeforeSpawnPlayer);

        Transform loc = SpawnPoints.instance.GetRandomSpawnForPlayer();

        GameObject obj = Instantiate(PlayerPref, loc.position, loc.localRotation);

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

            HealthBar.instance.SetValue(HealthBar.instance.SliderComponent.maxValue);

            PlayerInput.PlayersInputState = new InputSettings();

            StartCoroutine(SpawnPlayer());
        }
    }

    public void RepawnPlayer(Transform pos)
    {
        if (PlayerHealthData.instance.Health < 0 || PlayerHealthData.instance == null)
        {

            HealthBar.instance.SetValue(HealthBar.instance.SliderComponent.maxValue);

            PlayerInput.PlayersInputState = new InputSettings();

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
