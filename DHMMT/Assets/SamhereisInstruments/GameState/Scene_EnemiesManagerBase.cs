using Interfaces;
using System;

namespace GameStates.SceneManagers
{
    public abstract class Scene_EnemiesManagerBase<TSceneManager> : IInitializable where TSceneManager : Scene_SceneManagerBase
    {
        public Action<IDamagable> onEnemyKilled;

        protected TSceneManager _sceneManager;

        public Scene_EnemiesManagerBase(TSceneManager eFH_SceneManager)
        {
            _sceneManager = eFH_SceneManager;
        }

        public virtual void Initialize()
        {

        }

        public virtual void SubscribeToEvents()
        {

        }

        public virtual void UnsubscribeFromEvents()
        {

        }

        protected virtual void OnEnemyDied(IDamagable damagable)
        {
            onEnemyKilled?.Invoke(damagable);
        }
    }
}