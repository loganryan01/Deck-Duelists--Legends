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
                    GameLogRenderer.UpdateLog(game.Log);
                }

                //------------------------------------------------
                // Display KO animation
                //------------------------------------------------

                AnimationRenderer.DrawKOScreen();

                //------------------------------------------------
                // Redraw board and show defeat message
                //------------------------------------------------

                GameRenderer.DrawGameBoard(game.Player, game.Enemy, game.TurnNumber);

                GameUtils.ClearConsoleLine(GameConstants.BOX_FOUR_Y_POSITION);
                GameUtils.WriteAt(GameConstants.PLAYER_LOSE_MESSAGE, 0, GameConstants.BOX_FOUR_Y_POSITION);

                GameUtils.ClearConsoleLine(GameConstants.BOX_FOUR_Y_POSITION + 1);
                GameUtils.WriteAt(GameConstants.CONTINUE_MESSAGE, 0, GameConstants.BOX_FOUR_Y_POSITION + 1);

                // Clear input buffer
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }
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

                gameTurnStateManager.TransitionTo(new PlayerTurnState(), game);
            }
        }

        public override void Enter(Game game)
        {
            // Empty
        }

        public override void Exit(Game game)
        {
            // Empty
        }
    }
}