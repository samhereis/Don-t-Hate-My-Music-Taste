using DataClasses;
using DependencyInjection;
using ErtenGamesInstrumentals.DataClasses;
using GameState;
using GameStates.SceneManagers;
using Helpers;
using Identifiers;
using Interfaces;
using Music;
using Services;
using Servies;
using SO.Lists;
using UI.Windows;
using UnityEngine;

namespace GameStates
{
    public class MainMenuState : IGameState, INeedDependencyInjection, ISubscribesToEvents
    {
        [Inject] private PlayerInputService _inputContainer;
        [Inject] private SceneLoader _sceneLoader;
        [Inject] private ListOfAllScenes _listOfAllScenes;
        [Inject] private PlayingMusicData _playingMusicData;

        [Inject] private MainMenu_SceneManager _mainMenuSceneManager;

        [Inject] private IGameStateChanger _gameStatesManager;

        [Inject] private PrefabReference<LoadingMenu> _loadingMenu;

        private MainMenu _mainMenu;
        private SettingsMenu _settingsMenu;
        private SceneSelectionMenu _sceneSelectionMenu;

        private AScene_Extended _currentBackgroundScene;
        private GameObject _currentBackgroundSceneVisuals;

        public async void Enter()
        {
            var loadingMenu = Object.Instantiate(await _loadingMenu.GetAssetAsync());
            if (_loadingMenu != null) { Object.DontDestroyOnLoad(loadingMenu.gameObject); };

            await _sceneLoader.LoadSceneAsync(_listOfAllScenes.mainMenu, loadingMenu);

            SetupBackgroundScene();

            LoadUIs();
            SetupInput();

            _mainMenu?.Enable();

            SubscribeToEvents();
        }

        public void Exit()
        {
            UnsubscribeFromEvents();
        }

        public void SubscribeToEvents()
        {
            UnsubscribeFromEvents();

            _sceneSelectionMenu.onLoadSceneRequest += OnLoadSceneRequest;
        }

        public void UnsubscribeFromEvents()
        {
            _sceneSelectionMenu.onLoadSceneRequest -= OnLoadSceneRequest;
        }

        private void LoadUIs()
        {
            _mainMenu = Object.Instantiate<MainMenu>(_mainMenuSceneManager.mainMenuReference);
            _settingsMenu = Object.Instantiate<SettingsMenu>(_mainMenuSceneManager.settingsMenuReference);
            _sceneSelectionMenu = Object.Instantiate<SceneSelectionMenu>(_mainMenuSceneManager.selectMapMenuReference);

            _mainMenu.sceneSelectionMenu = _sceneSelectionMenu;
            _mainMenu.settingsMenu = _settingsMenu;

            _settingsMenu.openOnBack = _mainMenu;

            _sceneSelectionMenu.mainMenu = _mainMenu;
        }

        private void SetupBackgroundScene()
        {
            try
            {
                _currentBackgroundScene = _listOfAllScenes.GetScenes().GetRandom();
                if (_currentBackgroundScene == null || _currentBackgroundScene.backgroundSceneSettings.visuals == null) return;

                _currentBackgroundSceneVisuals = Object.Instantiate(_currentBackgroundScene.backgroundSceneSettings.visuals, Vector3.zero, Quaternion.identity);
                RenderSettings.skybox = _currentBackgroundScene.backgroundSceneSettings.skyboxes.GetRandom();
                RenderSettings.ambientIntensity = _currentBackgroundScene.backgroundSceneSettings.ambientIntencity;

                var cameraPosition = Object.FindFirstObjectByType<CameraPositionIdentifier_Identifier>(FindObjectsInactive.Include);
                _mainMenuSceneManager.cameraComponent.transform.SetParent(cameraPosition.transform, false);
                _mainMenuSceneManager.cameraComponent.transform.localPosition = Vector3.zero;

                var lightForMainMenu = Object.FindFirstObjectByType<LightForMainMenudentifier>(FindObjectsInactive.Include);
                if (lightForMainMenu != null)
                {
                    lightForMainMenu.transform.rotation = cameraPosition.lightTargetTransform.rotation;
                    lightForMainMenu.transform.position = cameraPosition.lightTargetTransform.position;
                }
            }
            catch
            {

            }
        }

        private void SetupInput()
        {
            _inputContainer.Enable();
            _inputContainer.input.Gameplay.Disable();
            _inputContainer.input.UI.Enable();
        }

        private void OnLoadSceneRequest(AScene_Extended scene)
        {
            switch (scene.gameMode)
            {
                case AScene_Extended.GameMode.EscapeFromHaters:
                    {
                        _gameStatesManager.ChangeState(new EFH_GameState(scene));

                        break;
                    }
                case AScene_Extended.GameMode.TsukuyomiDream:
                    {
                        _gameStatesManager.ChangeState(new TD_GameState(scene));

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