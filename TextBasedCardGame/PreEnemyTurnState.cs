using System;

namespace TextBasedCardGame
{
    /// <summary>
    /// Executes before the enemy's turn begins.
    /// 
    /// Responsible for checking win conditions
    /// before allowing the enemy to take their turn.
    /// </summary>
    public class PreEnemyTurnState : GameTurnState
    {
        public override void DoAction(Game game)
        {
            //------------------------------------------------
            // Check win condition
            // Player wins if their deck is empty
            //------------------------------------------------

            if (game.Enemy.Deck.IsEmpty())
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

                //------------------------------------------------
                // End the game
                //------------------------------------------------

                Console.ReadKey(true);

                game.StopGame();
            }
            else
            {
                //------------------------------------------------
                // Continue to enemy turn
                //------------------------------------------------

                gameTurnStateManager.TransitionTo(new EnemyTurnState());
            }
        }
    }
}
