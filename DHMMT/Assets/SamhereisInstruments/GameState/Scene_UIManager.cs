using DI;
using GameStates.SceneManagers;
using Interfaces;
using System;

namespace Managers.SceneManagers
{
    public abstract class Scene_UIManager<TSceneManager> : IInitializable, IClearable, ISubscribesToEvents where TSceneManager : Scene_SceneManagerBase
    {
        public Action<bool> onGamePauseStatusChanged;

        protected TSceneManager _sceneManager;

        public Scene_UIManager(TSceneManager tD_SceneManager)
        {
            _sceneManager = tD_SceneManager;
        }

        public virtual void Initialize()
        {
            (this as IDIDependent).LoadDependencies();
        }

        public virtual void Clear()
        {
            UnsubscribeFromEvents();
        }

        public virtual void SubscribeToEvents()
        {

        }

        public virtual void UnsubscribeFromEvents()
        {

        }

        protected virtual void PauseGame()
        {
            onGamePauseStatusChanged?.Invoke(true);
        }

        protected virtual void ResumeGame()
        {
            onGamePauseStatusChanged?.Invoke(false);
        }
    }
}