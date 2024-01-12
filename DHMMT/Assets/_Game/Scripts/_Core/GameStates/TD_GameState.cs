using Charatcers.Player;
using DataClasses;
using DependencyInjection;
using ErtenGamesInstrumentals.DataClasses;
using GameState;
using GameStates.SceneManagers;
using Helpers;
using Identifiers;
using Interfaces;
using Services;
using Servies;
using SO.Lists;
using UI.Windows;
using UnityEngine;

namespace GameStates
{
    public class TD_GameState : IGameState, INeedDependencyInjection
    {

        [Inject] private TD_SceneManager _tD_SceneManager;

        [Inject] private SceneLoader _sceneLoader;
        [Inject] private GameSaveService _gameSaveManager;
        [Inject] private ListOfAllScenes _listOfAllScenes;

        [Inject] private IGameStateChanger _gameStatesManager;

        private PlayerIdentifier _player;

        private TD_EnemiesManager _tD_EnemiesManager;
        private TD_UIManager _tD_UIManager;

        [Inject] private PrefabReference<LoadingMenu> _loadingMenu;

        private AScene_Extended _scene;

        public TD_GameState(AScene_Extended scene)
        {
            _scene = scene;
        }

        public async void Enter()
        {
            _sceneLoader = DependencyContext.diBox.Get<SceneLoader>();
            var loadingMenu = Object.Instantiate(await _loadingMenu.GetAssetAsync());
            if (_loadingMenu != null) { Object.DontDestroyOnLoad(loadingMenu.gameObject); };

            await _sceneLoader.LoadSceneAsync(_scene, loadingMenu);

            DependencyContext.diBox.InjectDataTo(this);

            _tD_SceneManager.Initialize();
            while (_tD_SceneManager.isInitialized == false) { await Awaitable.NextFrameAsync(); }

            SpawnPlayer();

            _tD_UIManager = new TD_UIManager(_tD_SceneManager);
            _tD_UIManager.Initialize();

            _tD_EnemiesManager = new TD_EnemiesManager(_tD_SceneManager);
            _tD_EnemiesManager.Initialize();

            SubscribeToEvents();

            Object.Destroy(loadingMenu.gameObject);
        }

        public void Exit()
        {
            UnsubscribeFromEvents();
            _tD_EnemiesManager.Clear();
            _tD_UIManager.Dispose();
        }

        public void SubscribeToEvents()
        {
            _tD_EnemiesManager.SubscribeToEvents();
            _tD_EnemiesManager.onEnemyDied.AddListener(OnEnemyKilled);

            _tD_UIManager.SubscribeToEvents();
            _tD_UIManager.onCountdownIsOver += Lose;

            _player.TryGet<PlayerHealth>().onDie += SpawnPlayer;

            _tD_UIManager.pauseMenu.onGoToMainMenuRequest += GoToMainMenu;
            _tD_UIManager.loseMenu.window.onGoToMainMenuRequest += GoToMainMenu;
        }

        public void UnsubscribeFromEvents()
        {
            _tD_EnemiesManager.UnsubscribeFromEvents();
            _tD_EnemiesManager.onEnemyDied.RemoveListener(OnEnemyKilled);

            _tD_UIManager.UnsubscribeFromEvents();
            _tD_UIManager.onCountdownIsOver -= Lose;

            _player.TryGet<PlayerHealth>().onDie -= SpawnPlayer;

            _tD_UIManager.pauseMenu.onGoToMainMenuRequest -= GoToMainMenu;
            _tD_UIManager.loseMenu.window.onGoToMainMenuRequest -= GoToMainMenu;
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
            var currentScene = _listOfAllScenes.lastLoadedScene;
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
            _tD_UIManager.loseMenu.window?.Enable();
            _tD_UIManager.loseMenu.window.onReplayRequest += Replay;
        }

        private void GoToMainMenu()
        {
            _gameStatesManager?.ChangeState(new MainMenuState());
        }

        private void Replay()
        {
            _tD_UIManager.loseMenu.window.onReplayRequest -= Replay;
            _gameStatesManager.ChangeState(this);
        }
    }
}