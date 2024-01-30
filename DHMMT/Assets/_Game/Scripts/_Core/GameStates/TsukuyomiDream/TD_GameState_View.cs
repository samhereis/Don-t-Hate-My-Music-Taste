using DependencyInjection;
using GameState;
using Interfaces;
using System;
using UI.Windows;
using UI.Windows.GameplayMenus;

using Object = UnityEngine.Object;

namespace GameStates.SceneManagers
{
    public class TD_GameState_View : GameState_ViewBase, ISubscribesToEvents, INeedDependencyInjection
    {
        public Action onCountdownIsOver;

        public TD_GameplayMenu gameplayMenu { get; private set; }
        public TD_LoseMenu loseMenu { get; private set; }
        public PauseMenu pauseMenu { get; private set; }

        TD_SceneManager _sceneManager;

        public TD_GameState_View(TD_SceneManager tD_SceneManager)
        {
            _sceneManager = tD_SceneManager;
        }

        public override void Initialize()
        {
            DependencyContext.diBox.InjectDataTo(this);

            gameplayMenu = Object.Instantiate(_sceneManager.modeTDGameplayMenuPrefab);
            pauseMenu = Object.Instantiate(_sceneManager.pauseMenuPrefab);
            loseMenu = Object.Instantiate(_sceneManager.modeLoseMenuPrefab);

            gameplayMenu?.window?.Enable();
        }

        public override void Dispose()
        {
            UnsubscribeFromEvents();
        }

        public void SubscribeToEvents()
        {
            if (gameplayMenu?.window != null)
            {
                gameplayMenu.window.onPauseRequested += PauseGame;

                gameplayMenu.onTimerOver += OnCountdownIsOver;
            }
        }

        public void UnsubscribeFromEvents()
        {
            if (gameplayMenu?.window != null)
            {
                gameplayMenu.window.onPauseRequested -= PauseGame;
                gameplayMenu.onTimerOver -= OnCountdownIsOver;
            }
        }

        protected void PauseGame()
        {
            pauseMenu?.Enable();

            pauseMenu.onResumeRequest -= ResumeGame;
            pauseMenu.onResumeRequest += ResumeGame;
        }

        protected void ResumeGame()
        {
            pauseMenu.onResumeRequest -= ResumeGame;

            gameplayMenu?.window?.Enable();
        }

        private void OnCountdownIsOver()
        {
            onCountdownIsOver?.Invoke();
        }
    }
}