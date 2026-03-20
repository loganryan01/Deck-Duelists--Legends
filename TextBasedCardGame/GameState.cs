namespace TextBasedCardGame
{
    /// <summary>
    /// Base Class for all game states.
    /// 
    /// Each state represents a different phase of the game
    /// (Menu, Play, etc.)
    /// </summary>
    public abstract class GameState
    {
        // Reference to the state manager that controls transitions
        protected GameStateManager gameStateManager;

        /// <summary>
        /// Assigns the GameStateManager to the state.
        /// This allows states to trigger transitions to other states.
        /// </summary>
        public void SetManager(GameStateManager gameStateManager)
        {
            this.gameStateManager = gameStateManager;
        }

        /// <summary>
        /// Executes the logic for the current state.
        /// Called each iteration of the main game loop.
        /// </summary>
        /// <param name="game"></param>
        public abstract void DoAction(Game game);
    }
}
