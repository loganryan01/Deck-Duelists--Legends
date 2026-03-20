using System;

namespace TextBasedCardGame
{
    /// <summary>
    /// Executes after the player's turn has finished.
    /// 
    /// Responsible for checking if the enemy hero has been defeated
    /// before continuing to the enemy turn.
    /// </summary>
    public class PostPlayerTurnState : GameTurnState
    {
        public override void DoAction(Game game)
        {
            //------------------------------------------------
            // Check win condition
            // Player wins if the enemy hero health reaches 0
            //------------------------------------------------

            if (game.Enemy.HeroHealth <= 0)
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
                GameUtils.WriteAt(GameConstants.PLAYER_WIN_MESSAGE, 0, 16);

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
                // Continue to enemy pre-turn state
                //------------------------------------------------

                gameTurnStateManager.TransitionTo(new PreEnemyTurnState());
            }
        }
    }
}
