using ConstStrings;
using DI;
using Interfaces;
using Music;
using System;
using UI.Windows;
using UI.Windows.GameplayMenus;

using Object = UnityEngine.Object;

namespace Managers.SceneManagers
{
    public class TD_UIManager : IInitializable, IClearable, ISubscribesToEvents, IDIDependent
    {
        public Action onGoToMainMenuRequest;

        public Action onCountdownIsOver;

        private TD_SceneManager _sceneManager;

        public TD_GameplayMenu gameplayMenu { get; private set; }
        public TD_LoseMenu loseMenu { get; private set; }
        public PauseMenu pauseMenu { get; private set; }

        [DI(DIStrings.playingMusicData)] private PlayingMusicData _playingMusicData;

        public TD_UIManager(TD_SceneManager tD_SceneManager)
        {
            _sceneManager = tD_SceneManager;
        }

        public void Initialize()
        {
            (this as IDIDependent).LoadDependencies();

            gameplayMenu = Object.Instantiate(_sceneManager.modeTDGameplayMenuPrefab);
            pauseMenu = Object.Instantiate(_sceneManager.pauseMenuPrefab);
            loseMenu = Object.Instantiate(_sceneManager.modeLoseMenuPrefab);

            gameplayMenu?.window.Initialize();
            loseMenu?.window.Initialize();
            pauseMenu?.Initialize();

            gameplayMenu?.window?.Enable();
            _playingMusicData?.SetActive(true, true);
        }

        public void Clear()
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

            if (pauseMenu != null)
            {
                pauseMenu.onGoToMainMenuRequest += GoToMainMenu;
            }

            if (loseMenu?.window != null)
            {
                loseMenu.window.onGoToMainMenuRequest += GoToMainMenu;
            }
        }

        public void UnsubscribeFromEvents()
        {
            if (gameplayMenu?.window != null)
            {
                gameplayMenu.window.onPauseRequested -= PauseGame;
                gameplayMenu.onTimerOver -= OnCountdownIsOver;
            }

            if (pauseMenu != null)
            {
                pauseMenu.onGoToMainMenuRequest -= GoToMainMenu;
            }

            if (loseMenu?.window != null)
            {
                loseMenu.window.onGoToMainMenuRequest -= GoToMainMenu;
            }
        }

        private void PauseGame()
        {
            pauseMenu?.Enable();

            pauseMenu.onResumeRequest -= ResumeGame;
            pauseMenu.onResumeRequest += ResumeGame;

            _playingMusicData.PauseMusic(true);
        }

        private void ResumeGame()
        {
            pauseMenu.onResumeRequest -= ResumeGame;

            gameplayMenu?.window?.Enable();

            _playingMusicData.PauseMusic(false);
        }

        private void GoToMainMenu()
        {
            onGoToMainMenuRequest?.Invoke();
        }

        private void OnCountdownIsOver()
        {
            onCountdownIsOver?.Invoke();
        }
    }
}