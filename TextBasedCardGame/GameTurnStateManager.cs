namespace TextBasedCardGame
{
    /// <summary>
    /// Manages the current turn state during gameplay.
    /// 
    /// This controls the flow of individual turns such as:
    /// - PrePlayerTurn
    /// - PlayerTurn
    /// - PostPlayerTurn
    /// - PreEnemyTurn
    /// - EnemyTurn
    /// - PostEnemyTurn
    /// </summary>
    public class GameTurnStateManager
    {
        // Current active turn state
        private GameTurnState state;

        /// <summary>
        /// Gets the current turn state.
        /// </summary>
        public GameTurnState State => state;

        /// <summary>
        /// Creates a new turn state manager with an initial state.
        /// </summary>
        public GameTurnStateManager(GameTurnState state, Game game)
        {
            TransitionTo(state, game);
        }

        /// <summary>
        /// Switches to a new turn state.
        /// </summary>
        public void TransitionTo(GameTurnState state, Game game)
        {
            if (this.state != null)
            {
                this.state.Exit(game);
            }
            this.state = state;
            this.state.Enter(game);

            // Give the state access to the manager
            // so it can trigger transitions.
            this.state.SetManager(this);
        }

        /// <summary>
        /// Executes the logic for the current turn state.
        /// Called each iteration of the turn loop.
        /// </summary>
        public void DoAction(Game game)
        {
            state.DoAction(game);
        }
    }
}
