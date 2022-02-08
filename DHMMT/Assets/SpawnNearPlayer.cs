using Events;
using Helpers;
using Identifiers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnNearPlayer : MonoBehaviour
{
    [SerializeField] private Transform _prefab;

    [Header("Events")]
    [SerializeField] private EventWithOneParameterBase<EnemyIdentifier> _eventWithOneParameter;
    [SerializeField] private EventWithNoParameters _eventWithNoParameters;

    [Header("Settings")]
    [SerializeField] private bool _spawnOnAwake;
    [SerializeField] private float _maxRadiusFromPlayer = 30f;
    [SerializeField] private float _minRadiusFromPlayer = 10f;

    private void Awake()
    {
        _eventWithNoParameters?.AdListener(Spawn);
        _eventWithOneParameter?.AdListener((x) => Spawn());
    }

    private void OnEnable()
    {
        if (_spawnOnAwake) Spawn();
    }

    public async void Spawn()
    {
        await AsyncHelper.Delay(0.2f);

        Transform newObj = Instantiate(_prefab);

        Vector3 p = Test();

        newObj.position =  new Vector3(p.x, p.y, p.z);
    }

    public Vector3 Test()
    {
        Vector3 player = PlayerIdentifier.instance.transform.position;

        float radius = Random.Range(_minRadiusFromPlayer, _maxRadiusFromPlayer);

        Vector3 randomDirection = Random.insideUnitSphere * radius;

        randomDirection += player;

        NavMeshHit hit;

        NavMesh.SamplePosition(randomDirection, out hit, radius, 1);

        Vector3 finalPosition = hit.position;

        return finalPosition;
    }
}
