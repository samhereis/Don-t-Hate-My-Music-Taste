using Charatcers.Player;
using ConstStrings;
using DataClasses;
using DependencyInjection;
using ErtenGamesInstrumentals.DataClasses;
using GameState;
using GameStates.SceneManagers;
using Helpers;
using Identifiers;
using Interfaces;
using Observables;
using Services;
using SO.Lists;
using System;
using System.Threading;
using UI.Windows;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace GameStates
{
    public class EFH_GameState_Controller : GameState_ControllerBase, INeedDependencyInjection, IDisposable
    {
        [Inject(DataSignal_ConstStrings.onExitFound)] private DataSignal<Exit_Identifier> _onExitFound;

        [Inject] private EFH_SceneManager _efh_SceneManager;

        [Inject] private SceneLoader _sceneLoader;
        [Inject] private ListOfAllScenes _listOfAllScenes;
        [Inject] private IGameStateChanger _gameStatesManager;
        [Inject] private PrefabReference<LoadingMenu> _loadingMenu;
        [Inject(DataSignal_ConstStrings.onGameplayStatusChaned)] private DataSignal<GameplayStatus> _onGameplayStatusChaned;

        private Exit_Identifier _exit;
        private TheLight_Identifier _theLight;
        private PlayerIdentifier _player;

        private EFH_GameState_EnemiesManager _efh_EnemiesManager;
        private EFH_GameState_View _efh_UIManager;

        private CancellationTokenSource _sessionCTS;

        private AScene_Extended _scene;

        public EFH_GameState_Controller(AScene_Extended scene)
        {
            _scene = scene;
        }

        public override async void Enter()
        {
            _sessionCTS = new CancellationTokenSource();

            _sceneLoader = DependencyContext.diBox.Get<SceneLoader>();
            var loadingMenu = Object.Instantiate(await _loadingMenu.GetAssetAsync());
            if (_loadingMenu != null) { Object.DontDestroyOnLoad(loadingMenu.gameObject); };

            await _sceneLoader.LoadSceneAsync(_scene, loadingMenu);

            DependencyContext.diBox.InjectDataTo(this);

            _efh_SceneManager.Initialize();
            while (_efh_SceneManager.isInitialized == false) { await Awaitable.NextFrameAsync(); }

            SpawnExit();
            SpawnTheLightAndPlayer();

            _efh_UIManager = new EFH_GameState_View(_efh_SceneManager);
            _efh_UIManager.Initialize();

            _efh_EnemiesManager = new EFH_GameState_EnemiesManager(_efh_SceneManager);
            _efh_EnemiesManager.Initialize(_player);

            SubscribeToEvents();

            CheckForIsWithinTheLight();

            Object.Destroy(loadingMenu);
        }

        public override void Exit()
        {
            UnsubscribeFromEvents();
        }

        public void SubscribeToEvents()
        {
            _efh_EnemiesManager.SubscribeToEvents();
            _efh_UIManager.SubscribeToEvents();

            _onExitFound.AddListener(Win);
            _player.TryGet<PlayerHealth>().onDie += Lose;

            _efh_EnemiesManager.onEnemyKilled += OnEnemyKilled;

            _efh_UIManager.pauseMenu.onGoToMainMenuRequest += GoToMainMenu;
            _efh_UIManager.loseMenu.window.onGoToMainMenuRequest += GoToMainMenu;
            _efh_UIManager.winMenu.window.onGoToMainMenuRequest += GoToMainMenu;

            _efh_UIManager.loseMenu.window.onReplayRequest += Replay;
            _efh_UIManager.winMenu.window.onReplayRequest += Replay;

            _onGameplayStatusChaned.AddListener(OnGamePauseStatusChanged);
        }

        public void UnsubscribeFromEvents()
        {
            _efh_EnemiesManager.UnsubscribeFromEvents();
            _efh_UIManager.UnsubscribeFromEvents();

            _onExitFound.RemoveListener(Win);
            _player.TryGet<PlayerHealth>().onDie -= Lose;

            _efh_EnemiesManager.onEnemyKilled -= OnEnemyKilled;

            _efh_UIManager.pauseMenu.onGoToMainMenuRequest -= GoToMainMenu;
            _efh_UIManager.loseMenu.window.onGoToMainMenuRequest -= GoToMainMenu;
            _efh_UIManager.winMenu.window.onGoToMainMenuRequest -= GoToMainMenu;

            _efh_UIManager.loseMenu.window.onReplayRequest -= Replay;
            _efh_UIManager.winMenu.window.onReplayRequest -= Replay;

            _onGameplayStatusChaned.RemoveListener(OnGamePauseStatusChanged);
        }

        private void SpawnExit()
        {
            if (_efh_SceneManager.exitLocations.Count > 0)
            {
                var randomExitLocation = _efh_SceneManager.exitLocations[Random.Range(0, _efh_SceneManager.exitLocations.Count)];

                _exit = Object.Instantiate(_efh_SceneManager.exitPrefab,
                    randomExitLocation.transform.position,
                    Quaternion.identity);
            }
        }

        private void SpawnTheLightAndPlayer()
        {
            if (_efh_SceneManager.theLightLocations.Count > 0)
            {
                var randomTheLightLocation = _efh_SceneManager.theLightLocations.GetRandom();

                if (_efh_SceneManager.isDebugMode == false)
                {
                    _theLight = Object.Instantiate(_efh_SceneManager.theLightPrefab,
                        randomTheLightLocation.transform.position,
                        Quaternion.identity);
                }

                _player = Object.Instantiate(_efh_SceneManager.playerPrefab,
                    randomTheLightLocation.transform.position,
                    Quaternion.identity);
            }
        }

        private async void CheckForIsWithinTheLight()
        {
            if (_efh_SceneManager.isDebugMode == true) { return; }

            int currentSeconds = _efh_SceneManager.secondsUntillLoseWhileOutsideOfTheLight;

            while (_sessionCTS.IsCancellationRequested == false)
            {
                await AsyncHelper.DelayFloat(1f);

                if (_theLight == null) { continue; }
                if (_player == null) { continue; }

                if (Vector3.Distance(_theLight.transform.position, _player.transform.position) > _efh_SceneManager.lightRange)
                {
                    OnPlayerIsOutsideLightRangeUpdate(ref currentSeconds);
                }
                else
                {
                    OnPlayerIsIntsideLightRangeUpdate(ref currentSeconds);
                }
            }
        }

        private void OnPlayerIsOutsideLightRangeUpdate(ref int currentSeconds)
        {
            currentSeconds--;

            if (_efh_UIManager.gameplayMenu.stayUnderTheLight_Popup.isEnabled == false)
            {
                _efh_UIManager.gameplayMenu.stayUnderTheLight_Popup.Enable();
            }

            _efh_UIManager.gameplayMenu.stayUnderTheLight_Popup.SetSeconds(currentSeconds);

            if (currentSeconds <= 0)
            {
                Lose();
            }
        }

        private void OnPlayerIsIntsideLightRangeUpdate(ref int currentSeconds)
        {
            currentSeconds = _efh_SceneManager.secondsUntillLoseWhileOutsideOfTheLight;

            if (_efh_UIManager.gameplayMenu.stayUnderTheLight_Popup.isEnabled == true)
            {
                _efh_UIManager.gameplayMenu.stayUnderTheLight_Popup.Disable();
            }
        }

        private void OnEnemyKilled(IDamagable enemy)
        {
            _efh_UIManager.gameplayMenu.window.IncreaseKillsCount(enemy);
        }

        private void OnGamePauseStatusChanged(GameplayStatus gameplayStatus)
        {
            bool isPause = gameplayStatus == GameplayStatus.Pause;

            var theLightNavMeshAgent = _theLight.TryGet<NavMeshAgent>();

            if (theLightNavMeshAgent != null)
            {
                theLightNavMeshAgent.isStopped = isPause;
            }
        }

        private void Lose()
        {
            Dispose();

            _efh_UIManager.loseMenu?.window.Enable();
        }

        private void Win(Exit_Identifier identifier)
        {
            Dispose();

            _efh_UIManager.winMenu?.window?.Enable();
        }

        private void GoToMainMenu()
        {
            _gameStatesManager?.ChangeState(new MainMenu_GameState_Controller());
        }

        private void Replay()
        {
            _gameStatesManager?.ChangeState(this);
        }

        public void Dispose()
        {
            _sessionCTS.Cancel();
        }
    }
}