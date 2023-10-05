using Charatcers.Enemy;
using ConstStrings;
using DI;
using Events;
using Helpers;
using Identifiers;
using IdentityCards;
using Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Managers.SceneManagers
{
    public class EscapeFromHaters_EnemiesManager : MonoBehaviour, IDIDependent, ISubscribesToEvents
    {
        [Header(HeaderStrings.prefabs)]
        [SerializeField] private List<EnemyIdentityCard> _enemiesToSpawnOnStart = new List<EnemyIdentityCard>();

        [Header(HeaderStrings.di)]
        [DI(Event_DIStrings.onEnemyDied)][SerializeField] private EventWithOneParameters<IDamagable> _onEnemyDied;

        [Header(HeaderStrings.settings)]
        [SerializeField] private LayerMask _enemyNavmeshLayerMask;

        private PlayerIdentifier _playerIdentifier;

        private void Start()
        {
            (this as IDIDependent).LoadDependencies();

            SubscribeToEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

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

        public void SubscribeToEvents()
        {
            _onEnemyDied.AddListener(OnEnemyDied);
        }

        public void UnsubscribeFromEvents()
        {
            _onEnemyDied.RemoveListener(OnEnemyDied);
        }

        private void OnEnemyDied(IDamagable damagable)
        {
            var enemyIdentifier = damagable.damagedObjectIdentifier.GetComponent<EnemyIdentifier>();
            var canSpawn = enemyIdentifier != null && enemyIdentifier.identityCard != null;

            if (canSpawn == true)
            {
                SpawnNearMainPlayer(enemyIdentifier.identityCard.target);
            }
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