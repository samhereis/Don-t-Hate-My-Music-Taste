using Charatcers.Player;
using ConstStrings;
using DataClasses;
using DI;
using GameStates.SceneManagers;
using Helpers;
using Identifiers;
using Interfaces;
using Managers;
using Managers.UIManagers;
using SamhereisTools;
using UI.Windows;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameStates
{
    public class TD_GameState : IGameState, IDIDependent
    {
        private AScene_Extended _scene;

        [DI(DIStrings.sceneManager_TD)] private TD_SceneManager _tD_SceneManager;

        [DI(DIStrings.sceneLoader)] private SceneLoader _sceneLoader;
        [DI(DIStrings.gameSaveManager)] private GameSaveManager _gameSaveManager;

        [DI] private GameStatesManager _gameStatesManager;

        private PlayerIdentifier _player;

        private TD_EnemiesManager _tD_EnemiesManager;
        private TD_UIManager _tD_UIManager;

        private LoadingMenu _loadingMenu;

        public TD_GameState()
        {

        }

        public TD_GameState(AScene_Extended scene) : base()
        {
            _scene = scene;
        }

        public async void Enter()
        {
            _sceneLoader = DIBox.Get<SceneLoader>(DIStrings.sceneLoader);
            _loadingMenu = await AddressablesHelper.InstantiateAsync<LoadingMenu>(AddressableStrings.loadingMenu);
            if (_loadingMenu != null) { Object.DontDestroyOnLoad(_loadingMenu.gameObject); };

            await SceneLoader.LoadSceneAsync(_scene, _sceneLoader, _loadingMenu);

            (this as IDIDependent).LoadDependencies();

            _tD_SceneManager.Initialize();
            while (_tD_SceneManager.isInitialized == false) { await Awaitable.NextFrameAsync(); }

            SpawnPlayer();

            _tD_UIManager = new TD_UIManager(_tD_SceneManager);
            _tD_UIManager.Initialize();

            _tD_EnemiesManager = new TD_EnemiesManager(_tD_SceneManager);
            _tD_EnemiesManager.Initialize();

            SubscribeToEvents();

            Addressables.ReleaseInstance(_loadingMenu.gameObject);
        }

        public void Exit()
        {
            UnsubscribeFromEvents();
        }

        public void SubscribeToEvents()
        {
            _tD_EnemiesManager.SubscribeToEvents();
            _tD_EnemiesManager.onEnemyKilled += OnEnemyKilled;

            _tD_UIManager.SubscribeToEvents();
            _tD_UIManager.onCountdownIsOver += Lose;

            _player.TryGet<PlayerHealth>().onDie += SpawnPlayer;

            _tD_UIManager.onGoToMainMenuRequest += GoToMainMenu;
        }

        public void UnsubscribeFromEvents()
        {
            _tD_EnemiesManager.UnsubscribeFromEvents();
            _tD_EnemiesManager.onEnemyKilled -= OnEnemyKilled;

            _tD_UIManager.UnsubscribeFromEvents();
            _tD_UIManager.onCountdownIsOver -= Lose;

            _player.TryGet<PlayerHealth>().onDie -= SpawnPlayer;

            _tD_UIManager.onGoToMainMenuRequest -= GoToMainMenu;
        }

        private void OnEnemyKilled(IDamagable enemy)
        {
            _tD_UIManager.gameplayMenu?.AddSeconds(_tD_SceneManager.secondsToGiveOnEnemyDie);
        }

        private void SpawnPlayer()
        {
            if (_tD_SceneManager.playerSpawnPoints.Length > 0)
            {
                _player = Object.Instantiate(_tD_SceneManager.playerPrefab, _tD_SceneManager.playerSpawnPoints.GetRandom().transform.position, Quaternion.identity);
            }
        }

        private void Lose()
        {
            Clear();

            var currentScene = _sceneLoader.lastLoadedScene;
            var modeTDSave = _gameSaveManager.modeTDSaves;
            var modeTDSaveUnit = modeTDSave.tD_Saves.Find(x => x.sceneName == currentScene.sceneCode);

            if (modeTDSaveUnit == null)
            {
                modeTDSaveUnit = new TD_SaveUnit();
                modeTDSaveUnit.sceneName = currentScene.sceneCode;

                modeTDSave.tD_Saves.SafeAdd(modeTDSaveUnit);
            }

            if (modeTDSaveUnit != null)
            {
                modeTDSaveUnit.record = _tD_UIManager.gameplayMenu.currentDuration;
            }

            _gameSaveManager.Save(_gameSaveManager.modeTDSaves);
            _tD_UIManager.loseMenu?.window?.Enable();
        }

        private void GoToMainMenu()
        {
            _gameStatesManager?.ChangeState<MainMenuState>(true);
        }

        private void Clear()
        {
            UnsubscribeFromEvents();
            _tD_EnemiesManager.Clear();
            _tD_UIManager.Clear();
        }
    }
}