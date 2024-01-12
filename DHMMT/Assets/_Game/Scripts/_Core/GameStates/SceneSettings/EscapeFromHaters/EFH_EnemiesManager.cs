using ConstStrings;
using DependencyInjection;
using GameState;
using Helpers;
using Identifiers;
using IdentityCards;
using Interfaces;
using Observables;
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameStates.SceneManagers
{
    public class EFH_EnemiesManager : GameState_EnemiesManagerBase, IInitializable<PlayerIdentifier>, INeedDependencyInjection
    {
        public Action<IDamagable> onEnemyKilled;

        [Inject(DataSignal_ConstStrings.onEnemyDied)] private DataSignal<IDamagable> _onEnemyDied;

        private List<EnemyIdentityCard> _enemiesToSpawnOnStart = new List<EnemyIdentityCard>();
        private LayerMask _enemyNavmeshLayerMask;

        private PlayerIdentifier _playerIdentifier;

        private EFH_SceneManager _sceneManager;

        public EFH_EnemiesManager(EFH_SceneManager eFH_SceneManager)
        {
            _sceneManager = eFH_SceneManager;

            _enemiesToSpawnOnStart = _sceneManager.enemiesToSpawnOnStart;
            _enemyNavmeshLayerMask = _sceneManager.enemyNavmeshLayerMask;
        }

        public void Initialize(PlayerIdentifier playerIdentifier)
        {
            DependencyContext.diBox.InjectDataTo(this);

            _playerIdentifier = playerIdentifier;

            if (_playerIdentifier == null) { _playerIdentifier = Object.FindFirstObjectByType<PlayerIdentifier>(FindObjectsInactive.Include); }

            if (_playerIdentifier != null)
            {
                foreach (var enemy in _enemiesToSpawnOnStart)
                {
                    SpawnNearMainPlayer(enemy.identityCard.target);
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

        protected void OnEnemyDied(IDamagable damagable)
        {
            Respawn(damagable);

            onEnemyKilled?.Invoke(damagable);
        }

        public void Respawn(IDamagable damagable)
        {
            SpawnNearMainPlayer(_enemiesToSpawnOnStart.GetRandom().identityCard.target);
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