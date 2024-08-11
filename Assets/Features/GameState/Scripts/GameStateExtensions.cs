namespace DefaultNamespace
{
    public static class GameStateExtensions
    {
        public static bool IsGameStartState(this GameState state)
        {
            return state.Name == "GameStartState";
        }
        
        public static bool IsGameLoseState(this GameState state)
        {
            return state.Name == "GameLoseState";
        }
        
        public static bool IsGameWinState(this GameState state)
        {
            return state.Name == "GameWinState";
        }
        
        public static bool IsGamePlayingState(this GameState state)
        {
            return state.Name == "GamePlayingState";
        }
    }
}