using GameState;
using UI.Windows;
using Object = UnityEngine.Object;

namespace GameStates
{
    public class MainMenu_GameState_View : GameState_ViewBase
    {
        private MainMenu_GameState_Model _model;

        private MainMenu _mainMenu;
        private SettingsMenu _settingsMenu;
        private SceneSelectionMenu _sceneSelectionMenu;

        public MainMenu_GameState_View(MainMenu_GameState_Model model)
        {
            _model = model;
        }

        public override void Initialize()
        {
            base.Initialize();

            LoadUIs();
        }

        private async void LoadUIs()
        {
            _mainMenu = Object.Instantiate<MainMenu>(await _model.listOfAllViews.GetViewAsync<MainMenu>());
            _settingsMenu = Object.Instantiate<SettingsMenu>(await _model.listOfAllViews.GetViewAsync<SettingsMenu>());
            _sceneSelectionMenu = Object.Instantiate<SceneSelectionMenu>(await _model.listOfAllViews.GetViewAsync<SceneSelectionMenu>());

            _mainMenu.Construct(_sceneSelectionMenu, _settingsMenu);
            _settingsMenu.Construct(_mainMenu);
            _sceneSelectionMenu.Construct(_model.listOfAllScenes, _model.onASceneSelected, _model.onASceneLoadRequested, _mainMenu);

            _mainMenu.Enable();
        }
    }
}