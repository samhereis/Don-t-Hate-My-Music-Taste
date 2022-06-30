using Identifiers;
using Samhereis.DI;
using Samhereis.Events;
using UnityEngine;
using UnityEngine.AI;

namespace Samhereis.Helpers
{
    public class SpawnNearPosition : MonoBehaviour, IDIDependent
    {
        [SerializeField] private Transform _prefab;

        [Header("Events")]
        [SerializeField] private EventWithNoParameters _eventWithNoParameters;
        [SerializeField] private EventWithOneParameterBase<EnemyIdentifier> _eventWithOneParameter;

        [Header("Settings")]
        [SerializeField] private bool _spawnOnAwake;
        [SerializeField] private float _spawnDelay = 0.2f;
        [SerializeField] private float _maxRadius = 30f;
        [SerializeField] private float _minRadius = 10f;

        [Samhereis.DI.DI(CharacterKeysContainer.mainPlayer)] [SerializeField] private IdentifierBase _player; 

        private void Awake()
        {
            _eventWithNoParameters?.AdListener(Spawn);
            _eventWithOneParameter?.AdListener(Spawn);
        }

        private void OnDestroy()
        {
            _eventWithNoParameters?.RemoveListener(Spawn);
            _eventWithOneParameter?.RemoveListener(Spawn);
        }

        private void OnEnable()
        {
            if (_spawnOnAwake) Spawn();
        }

        public Vector3 ApplyPosition(Vector3 position)
        {
            float radius = Random.Range(_minRadius, _maxRadius);

            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += position;

            NavMesh.SamplePosition(randomDirection, out var hit, radius, 1);
            Vector3 finalPosition = hit.position;

            return finalPosition;
        }
        private void Spawn(EnemyIdentifier obj)
        {
            Spawn();
        }

        public async void Spawn()
        {
            if (_player == null) await (this as IDIDependent).LoadDependencies();

            if (_player != null) await AsyncHelper.DelayAndDo(_spawnDelay, () => Instantiate(_prefab, ApplyPosition(_player.transform.position), Quaternion.identity));
        }
    }
}
