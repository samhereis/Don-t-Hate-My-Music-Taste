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
    public class EFH_UIManager : IInitializable, IClearable, ISubscribesToEvents, IDIDependent
    {
        public Action onGoToMainMenuRequest;

        private EFH_SceneManager _sceneManager;

        public EFH_GameplayMenu gameplayMenu { get; private set; }
        public EFH_LoseMenu loseMenu { get; private set; }
        public EFH_WinMenu winMenu { get; private set; }
        public PauseMenu pauseMenu { get; private set; }

        [DI(DIStrings.playingMusicData)] private PlayingMusicData _playingMusicData;

        public EFH_UIManager(EFH_SceneManager tD_SceneManager)
        {
            _sceneManager = tD_SceneManager;
        }

        public void Initialize()
        {
            (this as IDIDependent).LoadDependencies();

            gameplayMenu = Object.Instantiate(_sceneManager.gameplayMenuPrefab);
            winMenu = Object.Instantiate(_sceneManager.winMenuPrefab);
            loseMenu = Object.Instantiate(_sceneManager.loseMenuPrefab);
            pauseMenu = Object.Instantiate(_sceneManager.pauseMenuPrefab);

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
    }
}