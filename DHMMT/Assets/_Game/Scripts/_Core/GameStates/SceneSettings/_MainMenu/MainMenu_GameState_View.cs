using DataClasses;
using DependencyInjection;
using ErtenGamesInstrumentals.DataClasses;
using GameState;
using Interfaces;
using System;
using UI.Windows;
using Object = UnityEngine.Object;

namespace GameStates
{
    public class MainMenu_GameState_View : GameState_ViewBase, ISubscribesToEvents
    {
        public Action<AScene_Extended> onLoadSceneRequest;

        [Inject] private PrefabReference<MainMenu> _mainMenuReference;
        [Inject] private PrefabReference<SettingsMenu> _settingsMenuReference;
        [Inject] private PrefabReference<SceneSelectionMenu> _sceneSelectionMenuReference;

        private MainMenu _mainMenu;
        private SettingsMenu _settingsMenu;
        private SceneSelectionMenu _sceneSelectionMenu;

        public MainMenu_GameState_View()
        {

        }

        public override void Initialize()
        {
            base.Initialize();

            LoadUIs();
        }

        public void SubscribeToEvents()
        {
            UnsubscribeFromEvents();

            _sceneSelectionMenu.onLoadSceneRequest += onLoadSceneRequest;
        }

        public void UnsubscribeFromEvents()
        {
            _sceneSelectionMenu.onLoadSceneRequest -= onLoadSceneRequest;
        }

        private async void LoadUIs()
        {
            _mainMenu = Object.Instantiate<MainMenu>(await _mainMenuReference.GetAssetAsync());
            _settingsMenu = Object.Instantiate<SettingsMenu>(await _settingsMenuReference.GetAssetAsync());
            _sceneSelectionMenu = Object.Instantiate<SceneSelectionMenu>(await _sceneSelectionMenuReference.GetAssetAsync());

            _mainMenu.sceneSelectionMenu = _sceneSelectionMenu;
            _mainMenu.settingsMenu = _settingsMenu;

            _settingsMenu.openOnBack = _mainMenu;

            _sceneSelectionMenu.Construct(_mainMenu);
        }
    }
}