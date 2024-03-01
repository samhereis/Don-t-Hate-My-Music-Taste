using DataClasses;
using DependencyInjection;
using GameState;
using Managers;
using SO.Lists;

namespace GameStates
{
    public class MainMenu_GameState_Controller : GameState_ControllerBase, INeedDependencyInjection
    {
        [Inject] private IGameStateChanger _gameStatesManager;
        [Inject] private MainMenu_Scene _sceneManager;

        private ListOfAllScenes_Extended _listOfAllScenes;

        private MainMenu_GameState_Model _model;
        private MainMenu_GameState_View _view;

        public override async void Enter()
        {
            base.Enter();

            _listOfAllScenes = DependencyContext.diBox.Get<ListOfAllScenes_Extended>();
            await this.LoadSceneWithLoadingMenu(_listOfAllScenes.mainMenuScene);

            DependencyContext.InjectDependencies(this);
            _sceneManager.Initialize();

            _model = new MainMenu_GameState_Model();
            _view = new MainMenu_GameState_View(_model);

            _model.Initialize();
            _view.Initialize();

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

            _model.onASceneLoadRequested.AddListener(OnLoadSceneRequest);
        }

        public void UnsubscribeFromEvents()
        {
            _model.onASceneLoadRequested.RemoveListener(OnLoadSceneRequest);
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