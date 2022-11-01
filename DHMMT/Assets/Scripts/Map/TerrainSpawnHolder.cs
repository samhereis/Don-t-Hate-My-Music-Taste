using Identifiers;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(SpawnPoint))]
    public class TerrainSpawnHolder : MonoBehaviour
    {
        // When a terrain with a spawnPoint instantiates, add its spawnPoint to "SpawnPints" class

        [SerializeField] private SpawnPoint _spawnPoint;

        private void OnValidate()
        {
            _spawnPoint ??= GetComponent<SpawnPoint>();
        }

        private void Start()
        {

        }
    }
}