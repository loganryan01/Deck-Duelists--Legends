namespace TextBasedCardGame
{
    /// <summary>
    /// Base class for all turn states during gameplay.
    /// 
    /// Each state represents a specific phase of a turn such as:
    /// - PrePlayerTurn
    /// - PlayerTurn
    /// - PostPlayerTurn
    /// - PreEnemyTurn
    /// - EnemyTurn
    /// - PostEnemyTurn
    /// </summary>
    public abstract class GameTurnState
    {
        // Reference to the manager that controls turn state transitions
        protected GameTurnStateManager gameTurnStateManager;

        /// <summary>
        /// Assigns the GameTurnStateManager to the state.
        /// This allows the state to trigger transitions to other turn states.
        /// </summary>
        public void SetManager(GameTurnStateManager gameTurnStateManager)
        {
            this.gameTurnStateManager = gameTurnStateManager;
        }
        
        /// <summary>
        /// Executes the logic for the current turn state.
        /// This method must be implemented by all derived states.
        /// </summary>
        public abstract void DoAction(Game game);

        /// <summary>
        /// Executes when entering the state
        /// </summary>
        public abstract void Enter(Game game);

        /// <summary>
        /// Executes when exiting the state
        /// </summary>
        /// <param name="game"></param>
        public abstract void Exit(Game game);
    }
}
