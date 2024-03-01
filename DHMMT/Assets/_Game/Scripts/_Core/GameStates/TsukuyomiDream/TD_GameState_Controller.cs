using Charatcers.Player;
using DataClasses;
using DependencyInjection;
using GameState;
using GameStates.SceneManagers;
using Helpers;
using Identifiers;
using Interfaces;
using Services;
using SO.Lists;
using UnityEngine;

namespace GameStates
{
    public class TD_GameState_Controller : GameState_ControllerBase, INeedDependencyInjection
    {
        [Inject] private TD_SceneManager _tD_SceneManager;

        [Inject] private GameSaveService _gameSaveManager;
        [Inject] private ListOfAllScenes_Extended _listOfAllScenes;

        [Inject] private IGameStateChanger _gameStatesManager;

        private PlayerIdentifier _player;

        private TD_GameState_EnemiesManager _tD_EnemiesManager;
        private TD_GameState_View _tD_UIManager;

        private AScene_Extended _scene;

        public TD_GameState_Controller(AScene_Extended scene)
        {
            _scene = scene;
        }

        public async override void Enter()
        {
            await this.LoadSceneWithLoadingMenu(_scene);

            DependencyContext.diBox.InjectDataTo(this);

            _tD_SceneManager.Initialize();
            while (_tD_SceneManager.isInitialized == false) { await Awaitable.NextFrameAsync(); }

            SpawnPlayer();

            _tD_UIManager = new TD_GameState_View(_tD_SceneManager);
            _tD_UIManager.Initialize();

            _tD_EnemiesManager = new TD_GameState_EnemiesManager(_tD_SceneManager);
            _tD_EnemiesManager.Initialize();

            SubscribeToEvents();
        }

        public override void Exit()
        {
            try
            {
                UnsubscribeFromEvents();
                _tD_EnemiesManager.Dispose();
                _tD_UIManager.Dispose();
            }
            catch
            {
                Debug.LogError("Could not exit the state: " + GetType().Name);
            }
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
                _player = Object.Instantiate(_tD_SceneManager.playerPrefab,
                    _tD_SceneManager.playerSpawnPoints.GetRandom().transform.position,
                    Quaternion.identity);
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
            _gameStatesManager?.ChangeState(new MainMenu_GameState_Controller());
        }

        private void Replay()
        {
            _tD_UIManager.loseMenu.window.onReplayRequest -= Replay;
            _gameStatesManager.ChangeState(this);
        }
    }
}