using ConstStrings;
using DataClasses;
using DI;
using Interfaces;
using Managers.SceneManagers;
using Managers.UIManagers;
using Music;
using PlayerInputHolder;
using SamhereisTools;
using SO.Lists;
using UI.Windows;
using UnityEngine;

namespace GameStates
{
    public class MainMenuState : IGameState, IDIDependent, ISubscribesToEvents
    {
        [DI(DIStrings.inputHolder)] private Input_SO _inputContainer;
        [DI(DIStrings.sceneLoader)] private SceneLoader _sceneLoader;
        [DI(DIStrings.listOfAllScenes)] private ListOfAllScenes _listOfAllScenes;
        [DI(DIStrings.playingMusicData)] private PlayingMusicData _playingMusicData;

        [DI(DIStrings.sceneManager_MainMenu)] private MainMenu_SceneManager _mainMenuSceneManager;

        [DI] private GameStatesManager _gameStatesManager;

        private MainMenu _mainMenu;
        private SettingsMenu _settingsMenu;
        private SceneSelectionMenu _sceneSelectionMenu;

        public async void Enter()
        {
            _sceneLoader = DIBox.Get<SceneLoader>(DIStrings.sceneLoader);
            _listOfAllScenes = DIBox.Get<ListOfAllScenes>(DIStrings.listOfAllScenes);
            await _sceneLoader.LoadSceneAsync(_listOfAllScenes.mainMenu);

            (this as IDIDependent).LoadDependencies();

            LoadUIs();
            SetupInput();

            _mainMenu?.Enable();
            _playingMusicData?.SetActive(true, true);

            SubscribeToEvents();
        }

        public void Exit()
        {
            UnsubscribeFromEvents();
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

            _mainMenu?.Initialize();
            _settingsMenu?.Initialize();
            _sceneSelectionMenu?.Initialize();
        }

        private void SetupInput()
        {
            _inputContainer.Enable();
            _inputContainer.input.Gameplay.Disable();
            _inputContainer.input.UI.Enable();
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