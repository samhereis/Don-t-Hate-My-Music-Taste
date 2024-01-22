using ConstStrings;
using DependencyInjection;
using GameState;
using Helpers;
using Identifiers;
using IdentityCards;
using Interfaces;
using Observables;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameStates.SceneManagers
{
    public class TD_GameState_EnemiesManager : GameState_EnemiesManagerBase, ISubscribesToEvents, INeedDependencyInjection
    {
        [Inject(DataSignal_ConstStrings.onEnemyDied)] public DataSignal<IDamagable> onEnemyDied;

        private List<EnemyIdentityCard> enemiesToSpawnOnStart { get; set; } = new List<EnemyIdentityCard>();
        private EnemySpawnPoint_Identifier[] enemySpawnPoint_Identifiers { get; set; }

        private TD_SceneManager _sceneManager;

        public TD_GameState_EnemiesManager(TD_SceneManager eFH_SceneManager)
        {
            _sceneManager = eFH_SceneManager;
        }

        public override void Initialize()
        {
            DependencyContext.diBox.InjectDataTo(this);

            enemiesToSpawnOnStart = _sceneManager.enemiesToSpawnOnStart;
            enemySpawnPoint_Identifiers = _sceneManager.enemySpawnPoint_Identifiers;

            Spawn(enemiesToSpawnOnStart.GetRandom().identityCard.target);
        }

        public void Clear()
        {
            UnsubscribeFromEvents();
        }

        public void SubscribeToEvents()
        {
            onEnemyDied.AddListener(OnEnemyDied);
        }

        public void UnsubscribeFromEvents()
        {
            onEnemyDied.AddListener(OnEnemyDied);
        }

        protected void OnEnemyDied(IDamagable enemy)
        {
            onEnemyDied?.Invoke(enemy);

            Respawn(enemy);
        }

        public void Respawn(IDamagable damagable)
        {
            Spawn(enemiesToSpawnOnStart.GetRandom().identityCard.target);
        }

        private void Spawn(EnemyIdentifier enemyIdentifier)
        {
            if (enemyIdentifier == null) { return; }

            if (enemySpawnPoint_Identifiers.Length == 0) { enemySpawnPoint_Identifiers = Object.FindObjectsByType<EnemySpawnPoint_Identifier>(FindObjectsInactive.Include, FindObjectsSortMode.None); }
            if (enemySpawnPoint_Identifiers.Length == 0) { return; };

            if (enemySpawnPoint_Identifiers.Length > 0)
            {
                var position = enemySpawnPoint_Identifiers.GetRandom().transform.position;

                var enemyInstance = UnityEngine.Object.Instantiate<EnemyIdentifier>(enemyIdentifier, position, Quaternion.identity);
                enemyInstance.transform.position = position;
            }
        }
    }
}