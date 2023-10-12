using ConstStrings;
using DI;
using Managers.SceneManagers;
using Music;
using PlayerInputHolder;
using SamhereisTools;
using SO.Lists;
using UI.Windows;
using UnityEngine;

namespace GameStates
{
    public class MainMenuState : IGameState, IDIDependent
    {
        [DI(DIStrings.inputHolder)] private Input_SO _inputContainer;
        [DI(DIStrings.sceneLoader)] private SceneLoader _sceneLoader;
        [DI(DIStrings.listOfAllScenes)] private ListOfAllScenes _listOfAllScenes;
        [DI(DIStrings.playingMusicData)] private PlayingMusicData _playingMusicData;

        [DI(DIStrings.sceneSettings_MainMenu)] private MainMenu_SceneManager _mainMenuSceneManager;

        private MainMenu _mainMenu;
        private SettingsMenu _settingsMenu;
        private SceneSelectionMenu _sceneSelectionMenu;

        public async void Enter()
        {
            _sceneLoader = DIBox.Get<SceneLoader>(DIStrings.sceneLoader);
            _listOfAllScenes = DIBox.Get<ListOfAllScenes>(DIStrings.listOfAllScenes);
            await _sceneLoader.LoadSceneAsync(_listOfAllScenes.mainMenu);

            (this as IDIDependent).LoadDependencies();

            _mainMenu = Object.Instantiate<MainMenu>(_mainMenuSceneManager.mainMenuReference);
            _settingsMenu = Object.Instantiate<SettingsMenu>(_mainMenuSceneManager.settingsMenuReference);
            _sceneSelectionMenu = Object.Instantiate<SceneSelectionMenu>(_mainMenuSceneManager.selectMapMenuReference);

            _mainMenu.sceneSelectionMenu = _sceneSelectionMenu;
            _mainMenu.settingsMenu = _settingsMenu;

            _settingsMenu.openOnDisable = _mainMenu;

            _sceneSelectionMenu.mainMenu = _mainMenu;

            _mainMenu?.Initialize();
            _settingsMenu?.Initialize();
            _sceneSelectionMenu?.Initialize();

            _mainMenu?.Enable();
            _playingMusicData?.SetActive(true, true);

            SetupInput();
        }

        public void Exit()
        {

        }

        private void SetupInput()
        {
            _inputContainer.Enable();
            _inputContainer.input.Gameplay.Disable();
            _inputContainer.input.UI.Enable();
        }
    }
}