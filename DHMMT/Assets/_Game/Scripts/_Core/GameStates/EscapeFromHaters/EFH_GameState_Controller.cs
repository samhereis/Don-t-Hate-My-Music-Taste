using Charatcers.Player;
using DataClasses;
using DependencyInjection;
using ErtenGamesInstrumentals.DataClasses;
using GameState;
using GameStates.SceneManagers;
using Helpers;
using Identifiers;
using Interfaces;
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
        [Inject] private ListOfAllScenes_Extended _listOfAllScenes;
        [Inject] private IGameStateChanger _gameStatesManager;

        private EFH_GameState_Model _model;
        private EFH_GameState_View _efh_UIManager;
        private EFH_GameState_EnemiesManager _efh_EnemiesManager;

        private CancellationTokenSource _sessionCTS;

        private AScene_Extended _currentScene;

        public EFH_GameState_Controller(AScene_Extended scene)
        {
            _currentScene = scene;
        }

        public override async void Enter()
        {
            _sessionCTS = new CancellationTokenSource();

            DependencyContext.diBox.InjectDataTo(this);

            LoadingMenu loadingMenu = await this.LoadSceneWithLoadingMenu(_currentScene);

            _model = new EFH_GameState_Model();
            _efh_UIManager = new EFH_GameState_View(_model);
            _efh_EnemiesManager = new EFH_GameState_EnemiesManager(_model);

            _model.Initialize();
            _efh_UIManager.Initialize();
            _efh_EnemiesManager.Initialize();

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

            _model.onExitFound.AddListener(Win);
            _model.playerIdentifier.TryGet<PlayerHealth>().onDie += Lose;

            _model.onEnemyDied.AddListener(OnEnemyKilled);

            _efh_UIManager.pauseMenu.onGoToMainMenuRequest += GoToMainMenu;
            _efh_UIManager.loseMenu.window.onGoToMainMenuRequest += GoToMainMenu;
            _efh_UIManager.winMenu.window.onGoToMainMenuRequest += GoToMainMenu;

            _efh_UIManager.loseMenu.window.onReplayRequest += Replay;
            _efh_UIManager.winMenu.window.onReplayRequest += Replay;

            _model.onGameplayStatusChaned.AddListener(OnGamePauseStatusChanged);
        }

        public void UnsubscribeFromEvents()
        {
            _efh_EnemiesManager.UnsubscribeFromEvents();
            _efh_UIManager.UnsubscribeFromEvents();

            _model.onExitFound.RemoveListener(Win);
            _model.playerIdentifier.TryGet<PlayerHealth>().onDie -= Lose;

            _model.onEnemyDied.RemoveListener(OnEnemyKilled);

            _efh_UIManager.pauseMenu.onGoToMainMenuRequest -= GoToMainMenu;
            _efh_UIManager.loseMenu.window.onGoToMainMenuRequest -= GoToMainMenu;
            _efh_UIManager.winMenu.window.onGoToMainMenuRequest -= GoToMainMenu;

            _efh_UIManager.loseMenu.window.onReplayRequest -= Replay;
            _efh_UIManager.winMenu.window.onReplayRequest -= Replay;

            _model.onGameplayStatusChaned.RemoveListener(OnGamePauseStatusChanged);
        }

        private async void CheckForIsWithinTheLight()
        {
            if (_model.sceneManager.isDebugMode == true) { return; }

            int currentSeconds = _model.sceneManager.secondsUntillLoseWhileOutsideOfTheLight;

            while (_sessionCTS.IsCancellationRequested == false)
            {
                await AsyncHelper.DelayFloat(1f);

                if (_model.theLight == null) { continue; }
                if (_model.playerIdentifier == null) { continue; }

                if (Vector3.Distance(_model.theLight.transform.position,
                    _model.playerIdentifier.transform.position) > _model.sceneManager.lightRange)
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
            currentSeconds = _model.sceneManager.secondsUntillLoseWhileOutsideOfTheLight;

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

            var theLightNavMeshAgent = _model.theLight.TryGet<NavMeshAgent>();

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