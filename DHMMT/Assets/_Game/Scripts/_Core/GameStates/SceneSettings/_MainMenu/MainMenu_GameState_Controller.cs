using DataClasses;
using DependencyInjection;
using GameState;
using Managers;
using SO.Lists;

namespace GameStates
{
    public class MainMenu_GameState_Controller : GameState_ControllerBase, IGameState, INeedDependencyInjection
    {
        [Inject] private ListOfAllScenes _listOfAllScenes;

        [Inject] private IGameStateChanger _gameStatesManager;

        private MainMenu_GameState_View _view;

        public override async void Enter()
        {
            base.Enter();

            await this.LoadSceneWithLoadingMenu(_listOfAllScenes.mainMenu);

            SetupInput();
            SubscribeToEvents();
        }

        public override void Exit()
        {
            base.Exit();

            UnsubscribeFromEvents();
        }

        public void SubscribeToEvents()
        {
            UnsubscribeFromEvents();

            _view.onLoadSceneRequest += OnLoadSceneRequest;
        }

        public void UnsubscribeFromEvents()
        {
            _view.onLoadSceneRequest -= OnLoadSceneRequest;
        }

        private void SetupInput()
        {
            GlobalGameSettings.EnableUIMode();
        }

        private void OnLoadSceneRequest(AScene_Extended scene)
        {
            switch (scene.gameMode)
            {
                case AScene_Extended.GameMode.EscapeFromHaters:
                    {
                        _gameStatesManager.ChangeState(new EFH_GameState_Controller(scene));

                        break;
                    }
                case AScene_Extended.GameMode.TsukuyomiDream:
                    {
                        _gameStatesManager.ChangeState(new TD_GameState_Controller(scene));

                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
}