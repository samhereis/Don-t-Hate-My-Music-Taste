using Characters;
using Events;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

namespace Helpers
{
    public class SpawnNearPosition : MonoBehaviour
    {
        [SerializeField] private Transform _prefab;

        [Header("Events")]
        [SerializeField] private EventWithNoParameters _eventWithNoParameters;

        [Header("Settings")]
        [SerializeField] private bool _spawnOnAwake;
        [SerializeField] private float _spawnDelay = 0.2f;
        [SerializeField] private float _maxRadius = 30f;
        [SerializeField] private float _minRadius = 10f;

        private void Awake()
        {
            _eventWithNoParameters?.AdListener(Spawn);
        }

        private void Start()
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

        public async void Spawn()
        {
            try
            {
                await AsyncHelper.DelayAndDo(_spawnDelay, () => PhotonNetwork.Instantiate(_prefab.name, ApplyPosition(FindObjectOfType<LeadingAgent>(true).transform.position), Quaternion.identity));
            }
            finally
            {

            }
        }
    }
}
