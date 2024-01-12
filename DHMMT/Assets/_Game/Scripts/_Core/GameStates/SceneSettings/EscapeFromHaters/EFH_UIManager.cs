using DependencyInjection;
using GameState;
using Interfaces;
using Music;
using UI.Windows;
using UI.Windows.GameplayMenus;

using Object = UnityEngine.Object;

namespace GameStates.SceneManagers
{
    public class EFH_UIManager : GameState_ViewBase, ISubscribesToEvents, INeedDependencyInjection
    {
        public EFH_GameplayMenu gameplayMenu { get; private set; }
        public EFH_LoseMenu loseMenu { get; private set; }
        public EFH_WinMenu winMenu { get; private set; }
        public PauseMenu pauseMenu { get; private set; }

        [Inject] private PlayingMusicData _playingMusicData;

        private EFH_SceneManager _sceneManager;

        public EFH_UIManager(EFH_SceneManager tD_SceneManager)
        {
            _sceneManager = tD_SceneManager;
        }

        public override void Initialize()
        {
            DependencyContext.diBox.InjectDataTo(this);

            gameplayMenu = Object.Instantiate(_sceneManager.gameplayMenuPrefab);
            winMenu = Object.Instantiate(_sceneManager.winMenuPrefab);
            loseMenu = Object.Instantiate(_sceneManager.loseMenuPrefab);
            pauseMenu = Object.Instantiate(_sceneManager.pauseMenuPrefab);

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
            }
        }

        public void UnsubscribeFromEvents()
        {
            if (gameplayMenu?.window != null)
            {
                gameplayMenu.window.onPauseRequested -= PauseGame;
            }
        }

        protected void PauseGame()
        {
            pauseMenu?.Enable();

            pauseMenu.onResumeRequest -= ResumeGame;
            pauseMenu.onResumeRequest += ResumeGame;

            _playingMusicData.PauseMusic(true);
        }

        protected void ResumeGame()
        {
            pauseMenu.onResumeRequest -= ResumeGame;

            gameplayMenu?.window?.Enable();

            _playingMusicData.PauseMusic(false);
        }
    }
}