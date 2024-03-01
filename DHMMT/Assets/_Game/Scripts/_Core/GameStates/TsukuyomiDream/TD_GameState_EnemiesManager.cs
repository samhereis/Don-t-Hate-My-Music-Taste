using ConstStrings;
using DependencyInjection;
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
    public class TD_GameState_EnemiesManager : IInitializable, IDisposable, ISubscribesToEvents, INeedDependencyInjection
    {
        [Inject(DataSignal_ConstStrings.onEnemyDied)] public DataSignal<IDamagable> onEnemyDied;

        private List<EnemyIdentityCard> _enemiesToSpawnOnStart = new List<EnemyIdentityCard>();
        private EnemySpawnPoint_Identifier[] _enemySpawnPoint_Identifiers;

        private TD_SceneManager _sceneManager;

        public TD_GameState_EnemiesManager(TD_SceneManager eFH_SceneManager)
        {
            _sceneManager = eFH_SceneManager;
        }

        public void Initialize()
        {
            DependencyContext.diBox.InjectDataTo(this);

            _enemiesToSpawnOnStart = _sceneManager.enemiesToSpawnOnStart;
            _enemySpawnPoint_Identifiers = _sceneManager.enemySpawnPoint_Identifiers;

            Spawn(_enemiesToSpawnOnStart.GetRandom().identityCard.target);
        }

        public void Dispose()
        {
            UnsubscribeFromEvents();
        }

        public void SubscribeToEvents()
        {
            onEnemyDied.AddListener(Respawn);
        }

        public void UnsubscribeFromEvents()
        {
            onEnemyDied.AddListener(Respawn);
        }

        public void Respawn(IDamagable damagable)
        {
            Spawn(_enemiesToSpawnOnStart.GetRandom().identityCard.target);
        }

        private void Spawn(EnemyIdentifier enemyIdentifier)
        {
            if (enemyIdentifier == null) { return; }

            if (_enemySpawnPoint_Identifiers.Length == 0) { _enemySpawnPoint_Identifiers = Object.FindObjectsByType<EnemySpawnPoint_Identifier>(FindObjectsInactive.Include, FindObjectsSortMode.None); }
            if (_enemySpawnPoint_Identifiers.Length == 0) { return; };

            if (_enemySpawnPoint_Identifiers.Length > 0)
            {
                var position = _enemySpawnPoint_Identifiers.GetRandom().transform.position;

                var enemyInstance = UnityEngine.Object.Instantiate<EnemyIdentifier>(enemyIdentifier, position, Quaternion.identity);
                enemyInstance.transform.position = position;
            }
        }
    }
}