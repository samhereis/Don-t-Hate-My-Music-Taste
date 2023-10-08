using ConstStrings;
using Helpers;
using Identifiers;
using IdentityCards;
using Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Managers.SceneManagers
{
    public class EscapeFromHaters_EnemiesManager : MonoBehaviour
    {
        [Header(HeaderStrings.prefabs)]
        [SerializeField] private List<EnemyIdentityCard> _enemiesToSpawnOnStart = new List<EnemyIdentityCard>();

        [Header(HeaderStrings.settings)]
        [SerializeField] private LayerMask _enemyNavmeshLayerMask;

        private PlayerIdentifier _playerIdentifier;

        public void SpawnEnemies()
        {
            _playerIdentifier = FindFirstObjectByType<PlayerIdentifier>();

            if (_playerIdentifier != null)
            {
                foreach (var enemy in _enemiesToSpawnOnStart)
                {
                    SpawnNearMainPlayer(enemy.target);
                }
            }
        }

        public void Respawn(IDamagable damagable)
        {
            SpawnNearMainPlayer(_enemiesToSpawnOnStart.GetRandom().target);
        }

        private async void SpawnNearMainPlayer(EnemyIdentifier enemyIdentifier)
        {
            if (_playerIdentifier == null)
            {
                _playerIdentifier = FindFirstObjectByType<PlayerIdentifier>();
            }

            var canSpawn = _playerIdentifier != null;

            if (canSpawn)
            {
                var nearPositionToPlayer = await SpawnNearPositionUsingNavmesh.TryGetNearPositionWithAccess(_playerIdentifier.transform.position, 40, 50, _enemyNavmeshLayerMask);

                var enemyInstance = Instantiate(enemyIdentifier, nearPositionToPlayer, Quaternion.identity);
                enemyInstance.gameObject.SetActive(false);

                await AsyncHelper.Delay(1000);
                enemyInstance.gameObject.SetActive(true);
            }
        }
    }
}