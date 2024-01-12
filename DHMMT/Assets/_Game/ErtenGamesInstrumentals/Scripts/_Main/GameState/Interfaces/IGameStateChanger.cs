namespace GameState
{
    public interface IGameStateChanger
    {
        public void ChangeState(IGameState gameState);
    }
}