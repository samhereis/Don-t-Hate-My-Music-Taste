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
        private static Dictionary<Type, IGameState> _gameStates = new Dictionary<Type, IGameState>();
        private IGameState _currentGameState;

        private async void Awake()
        {
            DontDestroyOnLoad(gameObject);

            while (DependencyInjector.isGLoballyInjected == false)
            {
                await AsyncHelper.DelayFloat(1f);
            }

            (this as IDIDependent).LoadDependencies();

            ChangeState<MainMenuState>();
        }

        public void ChangeState<TGameState>(bool deletePrevious = false) where TGameState : IGameState, new()
        {
            if (_currentGameState is TGameState) { return; }

            _currentGameState?.Exit();
            if (deletePrevious) { _gameStates.Remove(typeof(TGameState)); }

            if (_gameStates.ContainsKey(typeof(TGameState)) == false) { _gameStates.Add(typeof(TGameState), new TGameState()); }
            var gameState = _gameStates[typeof(TGameState)];

            _currentGameState = gameState;
            _currentGameState?.Enter();
        }
    }
}