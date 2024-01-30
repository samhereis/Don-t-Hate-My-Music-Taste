using DependencyInjection;
using GameState;
using GameStates;
using Helpers;
using UnityEngine;

namespace Core
{
    public class GameBootstrap : GameBootstrapBase, INeedDependencyInjection
    {
        [SerializeField] private DependencyContext _globalDependencyContext;

        [Inject] private IGameStateChanger _gameStateChanger;

        private async void Start()
        {
            _globalDependencyContext.Initialize();

            while (DependencyContext.isGloballyInjected == false)
            {
                await AsyncHelper.Skip();
            }

            DependencyContext.InjectDependencies(this);

            _gameStateChanger.ChangeState(new MainMenu_GameState_Controller());
        }
    }
}