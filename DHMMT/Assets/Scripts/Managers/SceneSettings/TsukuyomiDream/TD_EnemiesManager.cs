using ConstStrings;
using DI;
using Events;
using Helpers;
using Identifiers;
using IdentityCards;
using Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Managers.SceneManagers
{
    public class TD_EnemiesManager : IInitializable, IClearable, ISubscribesToEvents, IDIDependent
    {
        public Action<IDamagable> onEnemyKilled;

        private TD_SceneManager _sceneManager;

        [DI(Event_DIStrings.onEnemyDied)] private EventWithOneParameters<IDamagable> _onEnemyDied;

        private List<EnemyIdentityCard> enemiesToSpawnOnStart { get; set; } = new List<EnemyIdentityCard>();
        private EnemySpawnPoint_Identifier[] enemySpawnPoint_Identifiers { get; set; }

        public TD_EnemiesManager(TD_SceneManager tD_SceneManager)
        {
            _sceneManager = tD_SceneManager;
        }

        public void Initialize()
        {
            (this as IDIDependent).LoadDependencies();

            enemiesToSpawnOnStart = _sceneManager.enemiesToSpawnOnStart;
            enemySpawnPoint_Identifiers = _sceneManager.enemySpawnPoint_Identifiers;

            Spawn(enemiesToSpawnOnStart.GetRandom().target);
        }

        public void Clear()
        {
            UnsubscribeFromEvents();
        }

        public void SubscribeToEvents()
        {
            _onEnemyDied.AddListener(OnEnemyKilled);
        }

        public void UnsubscribeFromEvents()
        {
            _onEnemyDied.AddListener(OnEnemyKilled);
        }

        private void OnEnemyKilled(IDamagable enemy)
        {
            onEnemyKilled?.Invoke(enemy);
            Respawn(enemy);
        }

        public void Respawn(IDamagable damagable)
        {
            Spawn(enemiesToSpawnOnStart.GetRandom().target);
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