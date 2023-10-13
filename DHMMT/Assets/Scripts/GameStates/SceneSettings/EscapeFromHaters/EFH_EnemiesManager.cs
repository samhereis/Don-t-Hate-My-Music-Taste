using ConstStrings;
using DI;
using Events;
using Helpers;
using Identifiers;
using IdentityCards;
using Interfaces;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameStates.SceneManagers
{
    public class EFH_EnemiesManager : Scene_EnemiesManagerBase<EFH_SceneManager>, IInitializable<PlayerIdentifier>, IDIDependent
    {
        [DI(Event_DIStrings.onEnemyDied)] private EventWithOneParameters<IDamagable> _onEnemyDied;

        private List<EnemyIdentityCard> _enemiesToSpawnOnStart = new List<EnemyIdentityCard>();
        private LayerMask _enemyNavmeshLayerMask;

        private PlayerIdentifier _playerIdentifier;

        public EFH_EnemiesManager(EFH_SceneManager eFH_SceneManager) : base(eFH_SceneManager)
        {
            _sceneManager = eFH_SceneManager;

            _enemiesToSpawnOnStart = _sceneManager.enemiesToSpawnOnStart;
            _enemyNavmeshLayerMask = _sceneManager.enemyNavmeshLayerMask;
        }

        public void Initialize(PlayerIdentifier playerIdentifier)
        {
            (this as IDIDependent).LoadDependencies();

            _playerIdentifier = playerIdentifier;

            if (_playerIdentifier == null) { _playerIdentifier = Object.FindFirstObjectByType<PlayerIdentifier>(FindObjectsInactive.Include); }

            if (_playerIdentifier != null)
            {
                foreach (var enemy in _enemiesToSpawnOnStart)
                {
                    SpawnNearMainPlayer(enemy.target);
                }
            }
        }

        public override void SubscribeToEvents()
        {
            _onEnemyDied.AddListener(OnEnemyDied);
        }

        public override void UnsubscribeFromEvents()
        {
            _onEnemyDied.RemoveListener(OnEnemyDied);
        }

        protected override void OnEnemyDied(IDamagable damagable)
        {
            Respawn(damagable);

            onEnemyKilled?.Invoke(damagable);
        }

        public void Respawn(IDamagable damagable)
        {
            SpawnNearMainPlayer(_enemiesToSpawnOnStart.GetRandom().target);
        }

        private async void SpawnNearMainPlayer(EnemyIdentifier enemyIdentifier)
        {
            var canSpawn = _playerIdentifier != null;

            if (canSpawn)
            {
                var nearPositionToPlayer = await SpawnNearPositionUsingNavmesh.TryGetNearPositionWithAccess(_playerIdentifier.transform.position, 40, 50, _enemyNavmeshLayerMask);

                var enemyInstance = Object.Instantiate(enemyIdentifier, nearPositionToPlayer, Quaternion.identity);
                enemyInstance.gameObject.SetActive(false);

                await AsyncHelper.DelayInt(1000);
                enemyInstance.gameObject.SetActive(true);
            }
        }
    }
}