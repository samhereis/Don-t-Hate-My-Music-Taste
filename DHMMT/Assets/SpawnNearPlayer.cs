using Events;
using Helpers;
using Identifiers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNearPlayer : MonoBehaviour
{
    [SerializeField] private Collider[] _spawnCollider;

    [SerializeField] private Transform _prefab;

    [Header("Events")]
    [SerializeField] private EventWithOneParameterBase<EnemyIdentifier> _eventWithOneParameter;
    [SerializeField] private EventWithNoParameters _eventWithNoParameters;

    [Header("Settings")]
    [SerializeField] private bool _spawnOnAwake;

    private void Awake()
    {
        _eventWithNoParameters?.AdListener(Spawn);
        _eventWithOneParameter?.AdListener((x) => Spawn());

        if (_spawnOnAwake) Spawn();
    }

    public async void Spawn()
    {
        await AsyncHelper.Delay(0.2f);

        Bounds bounds = _spawnCollider[CollectionsHelper.GetRandomIndex(_spawnCollider.Length)].bounds;

        float offsetX = Random.Range(-bounds.extents.x, bounds.extents.x);
        float offsetY = Random.Range(-bounds.extents.y, bounds.extents.y);
        float offsetZ = Random.Range(-bounds.extents.z, bounds.extents.z);

        Transform newObj = Instantiate(_prefab);

        newObj.position = bounds.center + new Vector3(offsetX, offsetY, offsetZ);
    }
}
