using System;

namespace TextBasedCardGame
{
    /// <summary>
    /// Executes after the enemy's turn has finished.
    /// 
    /// Responsible for checking if the player hero has been defeated
    /// before continuing to the next player turn.
    /// </summary>
    public class PostEnemyTurnState : GameTurnState
    {
        public override void DoAction(Game game)
        {
            //------------------------------------------------
            // Check loss condition
            // Player loses if their hero health reaches 0
            //------------------------------------------------

            if (game.Player.HeroHealth <= 0)
            {
                //------------------------------------------------
                // Update log if enabled
                //------------------------------------------------

                if (game.IsLogEnabled)
                {
                    GameUtils.UpdateLog(game.Log);
                }

                //------------------------------------------------
                // Display KO animation
                //------------------------------------------------

                GameUtils.DrawKOScreen();

                //------------------------------------------------
                // Redraw board and show defeat message
                //------------------------------------------------

                GameUtils.DrawGameBoard(game.Player, game.Enemy, game.TurnNumber);

                GameUtils.ClearConsoleLine(16);
                GameUtils.WriteAt(GameConstants.PLAYER_LOSE_MESSAGE, 0, 16);

                GameUtils.ClearConsoleLine(17);
                GameUtils.WriteAt(GameConstants.CONTINUE_MESSAGE, 0, 17);

                Console.ReadKey(true);

                //------------------------------------------------
                // End the game
                //------------------------------------------------

                game.StopGame();
            }
            else
            {
                //------------------------------------------------
                // Start the next player turn cycle
                //------------------------------------------------

                gameTurnStateManager.TransitionTo(new PrePlayerTurnState());
            }
        }
    }
}
