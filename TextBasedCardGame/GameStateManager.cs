namespace TextBasedCardGame
{
    /// <summary>
    /// Manages transitions between different game states.
    /// Uses the State Pattern to control game flow
    /// (Menu, Settings, Play, etc.).
    /// </summary>
    public class GameStateManager
    {
        // The current active game state
        private GameState gameState;

        /// <summary>
        /// Creates a new GameStateManager and sets the starting state.
        /// </summary>
        public GameStateManager(GameState gameState, Game game) 
        {
            TransitionTo(gameState, game);
        }

        /// <summary>
        /// Switches the game to a new state.
        /// </summary>
        /// <param name="gameState"></param>
        public void TransitionTo(GameState gameState, Game game)
        {
            if (this.gameState != null)
            {
                this.gameState.Exit(game);
            }
            this.gameState = gameState;
            this.gameState.Enter(game);

            // Give the state access to this manager so it can trigger transitions
            this.gameState.SetManager(this);
        }

        /// <summary>
        /// Executes the logic for the current game state.
        /// Called each iteration of the main game loop.
        /// </summary>
        /// <param name="game"></param>
        public void DoAction(Game game)
        {
            gameState.DoAction(game);
        }
    }
}
