using DependencyInjection;
using Helpers;
using Interfaces;
using System;
using UnityEngine;

namespace GameState
{
    [Serializable]
    public class SimpleGameStatesChanger : IGameStateChanger, IInitializable, INeedDependencyInjection
    {
        [field: SerializeField] public IGameState currentGameState { get; protected set; }

        public virtual async void Initialize()
        {
            while (DependencyContext.isGloballyInjected == false)
            {
                await AsyncHelper.DelayFloat(1f);
            }

            DependencyContext.InjectDependencies(this);
        }

        public virtual void ChangeState(IGameState gameState)
        {
            currentGameState?.Exit();

            DependencyContext.diBox.InjectDataTo(gameState);

            currentGameState = gameState;
            currentGameState.Enter();
        }
    }
}