using DependencyInjection;
using GameState;
using Interfaces;
using UI.Windows;
using UI.Windows.GameplayMenus;

using Object = UnityEngine.Object;

namespace GameStates.SceneManagers
{
    public class EFH_GameState_View : GameState_ViewBase, ISubscribesToEvents, INeedDependencyInjection
    {
        public EFH_GameplayMenu gameplayMenu { get; private set; }
        public EFH_LoseMenu loseMenu { get; private set; }
        public EFH_WinMenu winMenu { get; private set; }
        public PauseMenu pauseMenu { get; private set; }

        private EFH_GameState_Model _model;

        public EFH_GameState_View(EFH_GameState_Model model)
        {
            _model = model;
        }

        //TODO: Refactor this
        public override void Initialize()
        {
            DependencyContext.diBox.InjectDataTo(this);

            gameplayMenu = Object.Instantiate(_model.sceneManager.gameplayMenuPrefab);
            winMenu = Object.Instantiate(_model.sceneManager.winMenuPrefab);
            loseMenu = Object.Instantiate(_model.sceneManager.loseMenuPrefab);
            pauseMenu = Object.Instantiate(_model.sceneManager.pauseMenuPrefab);

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

            _model.playingMusicData.PauseMusic(true);
        }

        protected void ResumeGame()
        {
            pauseMenu.onResumeRequest -= ResumeGame;

            gameplayMenu?.window?.Enable();

            _model.playingMusicData.PauseMusic(false);
        }
    }
}