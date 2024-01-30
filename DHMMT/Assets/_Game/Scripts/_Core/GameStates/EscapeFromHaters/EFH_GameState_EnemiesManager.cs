using DependencyInjection;
using Helpers;
using Identifiers;
using Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameStates.SceneManagers
{
    public class EFH_GameState_EnemiesManager : IInitializable, INeedDependencyInjection
    {
        private EFH_GameState_Model _model;

        public EFH_GameState_EnemiesManager(EFH_GameState_Model model)
        {
            _model = model;
        }

        public void Initialize()
        {
            if (_model.playerIdentifier != null)
            {
                foreach (var enemy in _model.sceneManager.enemiesToSpawnOnStart)
                {
                    SpawnNearMainPlayer(enemy.identityCard.target);
                }
            }
        }

        public void SubscribeToEvents()
        {
            _model.onEnemyDied.AddListener(OnEnemyDied);
        }

        public void UnsubscribeFromEvents()
        {
            _model.onEnemyDied.RemoveListener(OnEnemyDied);
        }

        protected void OnEnemyDied(IDamagable damagable)
        {
            Respawn(damagable);
        }

        public void Respawn(IDamagable damagable)
        {
            SpawnNearMainPlayer(_model.sceneManager.enemiesToSpawnOnStart.GetRandom().identityCard.target);
        }

        private async void SpawnNearMainPlayer(EnemyIdentifier enemyIdentifier)
        {
            var canSpawn = _model.playerIdentifier != null;

            if (canSpawn)
            {
                var nearPositionToPlayer = await SpawnNearPositionUsingNavmesh.TryGetNearPositionWithAccess(
                    _model.playerIdentifier.transform.position,
                    40,
                    50,
                    _model.sceneManager.enemyNavmeshLayerMask);

                var enemyInstance = Object.Instantiate(enemyIdentifier, nearPositionToPlayer, Quaternion.identity);
                enemyInstance.gameObject.SetActive(false);

                await AsyncHelper.DelayInt(1000);
                enemyInstance.gameObject.SetActive(true);
            }
        }
    }
}