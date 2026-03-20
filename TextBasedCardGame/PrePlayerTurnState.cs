using System;

namespace TextBasedCardGame
{
    /// <summary>
    /// Executes before the player's turn begins.
    /// 
    /// Responsible for checking loss conditions
    /// before allowing the player to take their turn.
    /// </summary>
    public class PrePlayerTurnState : GameTurnState
    {
        public override void DoAction(Game game)
        {
            //------------------------------------------------
            // Check loss condition
            // Player loses if their deck is empty
            //------------------------------------------------

            if (game.Player.Deck.IsEmpty())
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
                // Continue to player's turn
                //------------------------------------------------

                gameTurnStateManager.TransitionTo(new PlayerTurnState());
            }
        }
    }
}