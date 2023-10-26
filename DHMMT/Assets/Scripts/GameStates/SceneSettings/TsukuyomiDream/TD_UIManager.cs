using ConstStrings;
using DI;
using Interfaces;
using Managers.SceneManagers;
using Music;
using System;
using UI.Windows;
using UI.Windows.GameplayMenus;

using Object = UnityEngine.Object;

namespace GameStates.SceneManagers
{
    public class TD_UIManager : Scene_UIManager<TD_SceneManager>, IInitializable, IClearable, ISubscribesToEvents, IDIDependent
    {
        public Action onCountdownIsOver;

        public TD_GameplayMenu gameplayMenu { get; private set; }
        public TD_LoseMenu loseMenu { get; private set; }
        public PauseMenu pauseMenu { get; private set; }

        [DI(DIStrings.playingMusicData)] private PlayingMusicData _playingMusicData;

        public TD_UIManager(TD_SceneManager tD_SceneManager) : base(tD_SceneManager)
        {

        }

        public override void Initialize()
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

        public override void Clear()
        {
            UnsubscribeFromEvents();
        }

        public override void SubscribeToEvents()
        {
            if (gameplayMenu?.window != null)
            {
                gameplayMenu.window.onPauseRequested += PauseGame;

                gameplayMenu.onTimerOver += OnCountdownIsOver;
            }
        }

        public override void UnsubscribeFromEvents()
        {
            if (gameplayMenu?.window != null)
            {
                gameplayMenu.window.onPauseRequested -= PauseGame;
                gameplayMenu.onTimerOver -= OnCountdownIsOver;
            }
        }

        protected override void PauseGame()
        {
            pauseMenu?.Enable();

            pauseMenu.onResumeRequest -= ResumeGame;
            pauseMenu.onResumeRequest += ResumeGame;

            _playingMusicData.PauseMusic(true);
        }

        protected override void ResumeGame()
        {
            pauseMenu.onResumeRequest -= ResumeGame;

            gameplayMenu?.window?.Enable();

            _playingMusicData.PauseMusic(false);
        }

        private void OnCountdownIsOver()
        {
            onCountdownIsOver?.Invoke();
        }
    }
}