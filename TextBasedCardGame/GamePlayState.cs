using System;

namespace TextBasedCardGame
{
    /// <summary>
    /// Handles the main gameplay loop for a round.
    /// Responsible for determining turn order, drawing cards,
    /// executing turns, and handling round results.
    /// </summary>
    public class GamePlayState : GameState
    {
        private static readonly Random random = new Random();
        
        public override void DoAction(Game game)
        {
            //------------------------------------------------
            // Determine which player goes first
            //------------------------------------------------

            EGameTurnState gameTurnState = DetermineFirstTurn();

            GameTurnState gameTurnStrategy = 
                gameTurnState == EGameTurnState.Player 
                ? new PrePlayerTurnState() 
                : new PreEnemyTurnState();

            GameTurnStateManager gameTurnStateManager = 
                new GameTurnStateManager(gameTurnStrategy);

            //------------------------------------------------
            // Draw initial screen for the first round
            //------------------------------------------------

            if (game.CurrentRound == 1)
            {
                Console.Clear();

                Console.WriteLine(GameConstants.SPLITTER_TEXT + "\n");
                Console.WriteLine(GameConstants.SPLITTER_TEXT + "\n\n\n\n\n\n\n\n");
                Console.WriteLine(GameConstants.SPLITTER_TEXT + "\n\n\n");
                Console.WriteLine(GameConstants.SPLITTER_TEXT);

                if (game.IsLogEnabled)
                {
                    GameUtils.DrawLog();
                }
            }

            //------------------------------------------------
            // Round start animation
            //------------------------------------------------

            DrawRoundScreen(game);

            //------------------------------------------------
            // Ensure player has 3 cards in hand
            //------------------------------------------------

            while (game.Player.Hand.Count < 3)
            {
                Card card = game.Player.Deck.DrawCard();
                game.Player.Hand.Add(card);
            }

            //------------------------------------------------
            // Display player hand
            //------------------------------------------------

            int index = 0;

            foreach (Card card in game.Player.Hand)
            {
                GameUtils.ClearConsoleLine(GameConstants.HAND_Y_POSITION + index);

                GameUtils.WriteAt(
                    string.Format(GameConstants.CARD_PRINT_FORMAT, index + 1, card.Name),
                    GameConstants.HAND_X_POSITION,
                    GameConstants.HAND_Y_POSITION + index
                );

                index++;
            }

            //------------------------------------------------
            // Main turn loop
            //------------------------------------------------

            while (game.IsPlaying)
            {
                gameTurnStateManager.DoAction(game);
            }

            //------------------------------------------------
            // Clear log after round ends
            //------------------------------------------------

            if (game.IsLogEnabled)
            {
                GameUtils.ClearLog();
            }

            //------------------------------------------------
            // Show round result
            //------------------------------------------------

            DrawPointScreen(game);

            game.ResetGame();

            //------------------------------------------------
            // Determine whether another round should start
            //------------------------------------------------

            bool continueRounds =
                game.CurrentFormat == GameConstants.GAME_FORMATS[0] && 
                game.CurrentRound < game.NumberOfRounds ||

                game.CurrentFormat == GameConstants.GAME_FORMATS[1] && 
                game.Player.Wins < game.NumberOfRounds && 
                game.Enemy.Wins < game.NumberOfRounds;

            if (continueRounds)
            {
                game.IncrementCurrentRound();
            }
            else
            {
                DrawPointWinnerScreen(game);

                game.Player.ResetWinCount();
                game.Enemy.ResetWinCount();

                Console.Clear();

                gameStateManager.TransitionTo(new GameMenuState());
            }
        }

        /// <summary>
        /// Randomly determines which player takes the first turn.
        /// </summary>
        /// <returns></returns>
        private static EGameTurnState DetermineFirstTurn()
        {
            return (EGameTurnState)random.Next(2);
        }

        //------------------------------------------------
        // Round Intro Screen
        //------------------------------------------------

        private void DrawRoundScreen(Game game)
        {
            ClearGameBoard();

            // Show round number or ready message
            GameUtils.WriteAt(
                    game.NumberOfRounds > 1 ? string.Format(GameConstants.ROUND_FORMAT, game.CurrentRound) : GameConstants.READY_MESSAGE,
                    GameConstants.MESSAGE_X_POSITION,
                    GameConstants.MESSAGE_Y_POSITION,
                    Alignment.Center
            );

            Thread.Sleep(GameConstants.THREE_SECOND_DELAY);

            GameUtils.ClearConsoleLine(GameConstants.MESSAGE_Y_POSITION);
            GameUtils.WriteAt(
                GameConstants.FIGHT_MESSAGE, 
                GameConstants.MESSAGE_X_POSITION, 
                GameConstants.MESSAGE_Y_POSITION, 
                Alignment.Center
            );

            Thread.Sleep(GameConstants.THREE_SECOND_DELAY);

            GameUtils.ClearConsoleLine(GameConstants.MESSAGE_Y_POSITION);
        }

        //------------------------------------------------
        // Round Result Screen
        //------------------------------------------------

        private void DrawPointScreen(Game game)
        {
            ClearGameBoard();

            GameUtils.WriteAt(GameConstants.POINTS_TITLE, 16, 1, Alignment.Center);

            GameUtils.WriteAt(
                string.Format(GameConstants.POINT_FORMAT, GameConstants.DEFAULT_PLAYER_NAME, game.Player.Wins, game.NumberOfRounds), 
                0, 
                6
            );

            GameUtils.WriteAt(
                string.Format(GameConstants.POINT_FORMAT, GameConstants.DEFAULT_ENEMY_NAME, game.Enemy.Wins, game.NumberOfRounds), 
                0, 
                6, 
                Alignment.Right
            );

            //------------------------------------------------
            // Determine round winner
            //------------------------------------------------

            if (game.Player.Deck.IsEmpty() || game.Player.HeroHealth <= 0)
            {
                game.Enemy.AddWin();
            }
            else if (game.Enemy.Deck.IsEmpty() || game.Enemy.HeroHealth <= 0)
            {
                game.Player.AddWin();
            }

            Thread.Sleep(GameConstants.THREE_SECOND_DELAY);

            GameUtils.ClearConsoleLine(6);

            GameUtils.WriteAt(
                string.Format(GameConstants.POINT_FORMAT, GameConstants.DEFAULT_PLAYER_NAME, game.Player.Wins, game.NumberOfRounds), 
                0, 
                6
            );

            GameUtils.WriteAt(
                string.Format(GameConstants.POINT_FORMAT, GameConstants.DEFAULT_ENEMY_NAME, game.Enemy.Wins, game.NumberOfRounds), 
                0, 
                6, 
                Alignment.Right
            );

            Thread.Sleep(GameConstants.THREE_SECOND_DELAY);
        }

        //------------------------------------------------
        // Match Winner Screen
        //------------------------------------------------

        private void DrawPointWinnerScreen(Game game)
        {
            GameUtils.WriteAt(GameConstants.POINTS_TITLE, 16, 1, Alignment.Center);

            if (game.Player.Wins > game.Enemy.Wins)
            {
                GameUtils.WriteAt(
                    string.Format(GameConstants.WINNER_FORMAT, GameConstants.DEFAULT_PLAYER_NAME), 
                    0, 
                    6
                );

                GameUtils.WriteAt(
                    string.Format(GameConstants.POINT_FORMAT, GameConstants.DEFAULT_ENEMY_NAME, game.Enemy.Wins, game.NumberOfRounds), 
                    0, 
                    6, 
                    Alignment.Right
                );
            }
            else
            {
                GameUtils.WriteAt(
                    string.Format(GameConstants.POINT_FORMAT, GameConstants.DEFAULT_PLAYER_NAME, game.Player.Wins, game.NumberOfRounds), 
                    0, 
                    6
                );

                GameUtils.WriteAt(
                    string.Format(GameConstants.WINNER_FORMAT, GameConstants.DEFAULT_ENEMY_NAME), 
                    0, 
                    6, 
                    Alignment.Right
                );
            }

            Console.SetCursorPosition(0, 16);
            Console.WriteLine(GameConstants.CONTINUE_MESSAGE);

            Console.ReadKey(true);

            Console.Clear();
        }

        //------------------------------------------------
        // Clear Game Board
        //------------------------------------------------

        private void ClearGameBoard()
        {
            GameUtils.ClearConsoleLine(GameConstants.TURN_Y_POSITION);

            int maxYPosition = GameConstants.PLAYER_STATS_Y_POSITION + GameConstants.PLAYER_STATS_BOX_WIDTH;
            for (int i = GameConstants.PLAYER_STATS_Y_POSITION; i < maxYPosition; i++)
                GameUtils.ClearConsoleLine(i);

            maxYPosition = GameConstants.HAND_Y_POSITION + GameConstants.HAND_BOX_WIDTH;
            for (int i = GameConstants.HAND_Y_POSITION; i < maxYPosition; i++)
                GameUtils.ClearConsoleLine(i);

            maxYPosition = GameConstants.ACTION_Y_POSITION + GameConstants.ACTION_BOX_WIDTH;
            for (int i = GameConstants.ACTION_Y_POSITION; i < maxYPosition; i++)
                GameUtils.ClearConsoleLine(i);
        }
    }
}
