using ConstStrings;
using DI;
using Helpers;
using Identifiers;
using IdentityCards;
using Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Managers.SceneManagers
{
    public class TD_EnemiesManager : MonoBehaviour, IDIDependent
    {
        [Header(HeaderStrings.prefabs)]
        [SerializeField] private List<EnemyIdentityCard> _enemiesToSpawnOnStart = new List<EnemyIdentityCard>();

        [Header(HeaderStrings.settings)]
        [SerializeField] private LayerMask _enemyNavmeshLayerMask;

        [Header(HeaderStrings.debug)]
        [SerializeField] private EnemySpawnPoint_Identifier[] _enemySpawnPoint_Identifiers;

        private PlayerIdentifier _playerIdentifier;

        public void SpawnEnemies()
        {
            _playerIdentifier = FindFirstObjectByType<PlayerIdentifier>();
            _enemySpawnPoint_Identifiers = FindObjectsByType<EnemySpawnPoint_Identifier>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            if (_playerIdentifier != null)
            {
                Spawn(_enemiesToSpawnOnStart.GetRandom().target);
            }
        }

        public void Respawn(IDamagable damagable)
        {
            Spawn(_enemiesToSpawnOnStart.GetRandom().target);
        }

        private void Spawn(EnemyIdentifier enemyIdentifier)
        {
            if (_enemySpawnPoint_Identifiers.Length > 0)
            {
                var position = _enemySpawnPoint_Identifiers.GetRandom().transform.position;

                var enemyInstance = Instantiate<EnemyIdentifier>(enemyIdentifier, position, Quaternion.identity);
                enemyInstance.transform.position = position;
            }
        }
    }
}