using ConstStrings;
using DI;
using Managers.SceneManagers;

namespace GameStates
{
    public class TD_GameState : IGameState, IDIDependent
    {
        [DI(Event_DIStrings.onEnemyDied)] private TD_SceneManager _tD_SceneManager;

        public void Enter()
        {

        }
        public void Exit()
        {

        }
    }
}