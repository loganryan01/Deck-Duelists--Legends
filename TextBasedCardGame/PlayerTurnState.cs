using System;

namespace TextBasedCardGame
{
    /// <summary>
    /// Handles the player's turn.
    /// Responsible for displaying the board, drawing cards,
    /// processing player input, and applying card effects.
    /// </summary>
    public class PlayerTurnState : GameTurnState
    {
        public override void DoAction(Game game)
        {
            //------------------------------------------------
            // Draw game board
            //------------------------------------------------

            GameRenderer.DrawGameBoard(game.Player, game.Enemy, game.TurnNumber);

            if (game.IsLogEnabled)
            {
                GameLogRenderer.UpdateLog(game.Log);
            }

            //------------------------------------------------
            // Ensure player has 3 cards
            //------------------------------------------------

            while (game.Player.Hand.Count < GameConstants.MAX_HAND_SIZE)
            {
                Card card = game.Player.Deck.DrawCard();
                game.Player.Hand.Add(card);
            }

            //------------------------------------------------
            // Display player's hand
            //------------------------------------------------

            int index = 0;

            foreach (Card card in game.Player.Hand)
            {
                GameUtils.ClearConsoleLine(GameConstants.BOX_THREE_Y_POSITION + index);

                GameUtils.WriteAt(
                    string.Format(GameConstants.CARD_PRINT_FORMAT, index + 1, card.Name), 
                    0,
                    GameConstants.BOX_THREE_Y_POSITION + index
                );

                index++;
            }

            //------------------------------------------------
            // Ask player which card to play
            //------------------------------------------------

            GameUtils.ClearConsoleLine(GameConstants.BOX_FOUR_Y_POSITION);
            GameUtils.WriteAt(GameConstants.PLAYER_CHOOSE_MESSAGE, 0, GameConstants.BOX_FOUR_Y_POSITION);

            bool successfulInput = false;

            while (!successfulInput)
            {
                try
                {
                    Console.CursorVisible = true;

                    int chosenCardIndex = Convert.ToInt32(Console.ReadLine()) - 1;

                    if (chosenCardIndex >= 0 && chosenCardIndex <= game.Player.Hand.Count)
                    {
                        Console.CursorVisible = false;
                        successfulInput = true;

                        PlayCard(game, chosenCardIndex);
                    }
                    else
                    {
                        PrintInvalidInput();
                    }
                }
                catch (Exception)
                {
                    PrintInvalidInput();
                }
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

        //------------------------------------------------
        // Card Resolution
        //------------------------------------------------
        private void PlayCard(Game game, int chosenCardIndex)
        {
            Card chosenCard = game.Player.Hand[chosenCardIndex];

            //------------------------------------------------
            // Apply card effect
            //------------------------------------------------

            chosenCard.Play(game, game.Player, game.Enemy);

            //------------------------------------------------
            // Log action
            //------------------------------------------------

            game.AddToLog(GameConstants.DEFAULT_PLAYER_NAME, chosenCard.Name);

            //------------------------------------------------
            // Remove card from hand
            //------------------------------------------------

            game.Player.Hand.RemoveAt(chosenCardIndex);

            //------------------------------------------------
            // Player attacks enemy hero
            //------------------------------------------------

            if (game.Player.HeroAttack > GameConstants.MINIMUM_ATTACK_NUMBER)
            {
                game.Enemy.ModifyHeroHealth(-game.Player.HeroAttack);
            }

            //------------------------------------------------
            // Redraw player hand
            //------------------------------------------------

            for (int i = 0; i < GameConstants.MAX_HAND_SIZE; i++)
            {
                GameUtils.ClearConsoleLine(GameConstants.BOX_THREE_Y_POSITION + i);

                if (i < game.Player.Hand.Count)
                {
                    Card card = game.Player.Hand[i];

                    GameUtils.WriteAt(
                        string.Format(GameConstants.CARD_PRINT_FORMAT, i + 1, card.Name), 
                        0, 
                        GameConstants.BOX_THREE_Y_POSITION + i
                    );
                }
            }

            //------------------------------------------------
            // Clear input area
            //------------------------------------------------

            GameUtils.ClearConsoleLine(GameConstants.BOX_FOUR_Y_POSITION);
            GameUtils.ClearConsoleLine(GameConstants.BOX_FOUR_Y_POSITION + 1);

            //------------------------------------------------
            // Move to next turn state
            //------------------------------------------------

            gameTurnStateManager.TransitionTo(new PostPlayerTurnState(), game);

            // Increment turn number
            game.IncrementTurnNumber();
        }

        //------------------------------------------------
        // Error handling
        //------------------------------------------------

        private void PrintInvalidInput()
        {
            GameUtils.ClearConsoleLine(GameConstants.BOX_FOUR_Y_POSITION);
            GameUtils.ClearConsoleLine(GameConstants.BOX_FOUR_Y_POSITION + 1);

            GameUtils.WriteAt(GameConstants.INVALID_INPUT_MESSAGE, 0, GameConstants.BOX_FOUR_Y_POSITION);
        }
    }
}
