using DI;
using GameStates;
using Helpers;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers.UIManagers
{
    public class GameStatesManager : MonoBehaviour, IDIDependent
    {
        private Dictionary<Type, IGameState> _gameStates = new Dictionary<Type, IGameState>();
        private IGameState _currentGameState;

        private async void Awake()
        {
            var gameStatesManagers = FindObjectsByType<GameStatesManager>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            if (gameStatesManagers.Length > 1)
            {
                Destroy(gameObject);
                return;
            }

            transform.SetParent(null);

            DontDestroyOnLoad(gameObject);

            while (DependencyInjector.isGLoballyInjected == false)
            {
                await AsyncHelper.DelayFloat(1f);
            }

            (this as IDIDependent).LoadDependencies();

            ChangeState<MainMenuState>();
        }

        public IGameState GetState<TGameState>() where TGameState : IGameState, new()
        {
            SetupStateIfNull<TGameState>();
            var gameState = _gameStates[typeof(TGameState)];

            return gameState;
        }

        public void ChangeState<TGameState>(bool deletePrevious = true) where TGameState : IGameState, new()
        {
            ChangeState(GetState<TGameState>(), deletePrevious);
        }

        public void ChangeState(IGameState gameState, bool deletePrevious = true)
        {
            if (_currentGameState == gameState) { return; }

            _currentGameState?.Exit();
            if (deletePrevious) { _gameStates.Remove(gameState.GetType()); }

            if (_gameStates.ContainsKey(gameState.GetType()) == false)
            {
                { _gameStates.Add(gameState.GetType(), gameState); }
            }
            else
            {
                _gameStates[gameState.GetType()] = gameState;
            }

            _currentGameState = gameState;
            _currentGameState?.Enter();
        }

        private void SetupStateIfNull<TGameState>() where TGameState : IGameState, new()
        {
            if (_gameStates.ContainsKey(typeof(TGameState)) == false) { _gameStates.Add(typeof(TGameState), new TGameState()); }
        }
    }
}