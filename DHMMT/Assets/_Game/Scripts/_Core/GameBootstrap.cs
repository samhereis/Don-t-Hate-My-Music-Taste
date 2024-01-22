using DependencyInjection;
using GameState;
using GameStates;
using Helpers;

namespace Core
{
    public class GameBootstrap : GameBootstrapBase, INeedDependencyInjection
    {
        [Inject] private IGameStateChanger _gameStateChanger;

        private async void Start()
        {
            while(DependencyContext.isGloballyInjected == false)
            {
                await AsyncHelper.Skip();
            }

            DependencyContext.InjectDependencies(this);

            _gameStateChanger.ChangeState(new MainMenu_GameState_Controller());
        }
    }
}