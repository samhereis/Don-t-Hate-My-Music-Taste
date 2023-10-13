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
    public class TD_EnemiesManager : Scene_EnemiesManagerBase<TD_SceneManager>, IInitializable, IClearable, ISubscribesToEvents, IDIDependent
    {
        [DI(Event_DIStrings.onEnemyDied)] private EventWithOneParameters<IDamagable> _onEnemyDied;

        public TD_EnemiesManager(TD_SceneManager eFH_SceneManager) : base(eFH_SceneManager)
        {

        }

        private List<EnemyIdentityCard> enemiesToSpawnOnStart { get; set; } = new List<EnemyIdentityCard>();
        private EnemySpawnPoint_Identifier[] enemySpawnPoint_Identifiers { get; set; }

        public override void Initialize()
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

        public override void SubscribeToEvents()
        {
            _onEnemyDied.AddListener(OnEnemyDied);
        }

        public override void UnsubscribeFromEvents()
        {
            _onEnemyDied.AddListener(OnEnemyDied);
        }

        protected override void OnEnemyDied(IDamagable enemy)
        {
            base.OnEnemyDied(enemy);

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