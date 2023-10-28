using Charatcers.Player;
using ConstStrings;
using DataClasses;
using DI;
using Events;
using GameStates.SceneManagers;
using Helpers;
using Identifiers;
using Interfaces;
using Managers.UIManagers;
using SamhereisTools;
using SO.Lists;
using System.Threading;
using UI.Windows;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AI;

namespace GameStates
{
    public class EFH_GameState : IGameState, IDIDependent, IClearable
    {
        private AScene_Extended _scene;

        [DI(DIStrings.sceneManager_EFH)] private EFH_SceneManager _efh_SceneManager;

        [DI(Event_DIStrings.onExitFound)] private EventWithOneParameters<Exit_Identifier> _onExitFound;

        [DI(DIStrings.sceneLoader)] private SceneLoader _sceneLoader;
        [DI(DIStrings.listOfAllScenes)] private ListOfAllScenes _listOfAllScenes;

        [DI] private GameStatesManager _gameStatesManager;

        private Exit_Identifier _exit;
        private TheLight_Identifier _theLight;
        private PlayerIdentifier _player;

        private LoadingMenu _loadingMenu;

        private EFH_EnemiesManager _efh_EnemiesManager;
        private EFH_UIManager _efh_UIManager;

        private CancellationTokenSource _sessionCTS;

        public EFH_GameState()
        {

        }

        public EFH_GameState(AScene_Extended scene) : base()
        {
            _scene = scene;
        }

        public async void Enter()
        {
            _sessionCTS = new CancellationTokenSource();

            _sceneLoader = DIBox.Get<SceneLoader>(DIStrings.sceneLoader);
            _loadingMenu = await AddressablesHelper.InstantiateAsync<LoadingMenu>(AddressableStrings.loadingMenu);
            if (_loadingMenu != null) { Object.DontDestroyOnLoad(_loadingMenu.gameObject); };

            await SceneLoader.LoadSceneAsync(_scene, _sceneLoader, _loadingMenu);

            (this as IDIDependent).LoadDependencies();

            _efh_SceneManager.Initialize();
            while (_efh_SceneManager.isInitialized == false) { await Awaitable.NextFrameAsync(); }

            SpawnExit();
            SpawnTheLightAndPlayer();

            _efh_UIManager = new EFH_UIManager(_efh_SceneManager);
            _efh_UIManager.Initialize();

            _efh_EnemiesManager = new EFH_EnemiesManager(_efh_SceneManager);
            _efh_EnemiesManager.Initialize(_player);

            SubscribeToEvents();

            Addressables.ReleaseInstance(_loadingMenu.gameObject);

            CheckForIsWithinTheLight();
        }

        public void Exit()
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

            _efh_UIManager.onGamePauseStatusChanged += OnGamePauseStatusChanged;
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

            _efh_UIManager.onGamePauseStatusChanged -= OnGamePauseStatusChanged;
        }

        private void SpawnExit()
        {
            if (_efh_SceneManager.exitLocations.Count > 0)
            {
                var randomExitLocation = _efh_SceneManager.exitLocations[Random.Range(0, _efh_SceneManager.exitLocations.Count)];

                _exit = Object.Instantiate(_efh_SceneManager.exitPrefab, randomExitLocation.transform.position, Quaternion.identity);
            }
        }

        private void SpawnTheLightAndPlayer()
        {
            if (_efh_SceneManager.theLightLocations.Count > 0)
            {
                var randomTheLightLocation = _efh_SceneManager.theLightLocations.GetRandom();

                if (_efh_SceneManager.isDebugMode == false) { _theLight = Object.Instantiate(_efh_SceneManager.theLightPrefab, randomTheLightLocation.transform.position, Quaternion.identity); }
                _player = Object.Instantiate(_efh_SceneManager.playerPrefab, randomTheLightLocation.transform.position, Quaternion.identity);
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

        private void OnGamePauseStatusChanged(bool isPaused)
        {
            var theLightNavMeshAgent = _theLight.TryGet<NavMeshAgent>();

            if (theLightNavMeshAgent != null)
            {
                theLightNavMeshAgent.isStopped = isPaused;
            }
        }

        private void Lose()
        {
            Clear();

            _efh_UIManager.loseMenu?.window.Enable();
        }

        private void Win(Exit_Identifier identifier)
        {
            Clear();

            _efh_UIManager.winMenu?.window?.Enable();
        }

        private void GoToMainMenu()
        {
            _gameStatesManager?.ChangeState<MainMenuState>(true);
        }

        private void Replay()
        {
            _gameStatesManager?.ChangeState(this, isReenter: true);
        }

        public void Clear()
        {
            _sessionCTS.Cancel();
        }
    }
}