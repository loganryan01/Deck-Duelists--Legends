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
                new GameTurnStateManager(gameTurnStrategy, game);

            //------------------------------------------------
            // Draw initial screen for the first round
            //------------------------------------------------

            if (game.CurrentRound == 1)
            {
                GameUtils.WriteAt(GameConstants.SPLITTER_TEXT, 0, 0);
                GameUtils.WriteAt(GameConstants.SPLITTER_TEXT, 0, GameConstants.BOX_ONE_Y_POSITION + GameConstants.BOX_ONE_WIDTH);
                GameUtils.WriteAt(GameConstants.SPLITTER_TEXT, 0, GameConstants.BOX_TWO_Y_POSITION + GameConstants.BOX_TWO_WIDTH);
                GameUtils.WriteAt(GameConstants.SPLITTER_TEXT, 0, GameConstants.BOX_THREE_Y_POSITION + GameConstants.BOX_THREE_WIDTH);

                if (game.IsLogEnabled)
                {
                    GameLogRenderer.DrawLog();
                }
            }

            //------------------------------------------------
            // Round start animation
            //------------------------------------------------

            DrawRoundScreen(game);

            //------------------------------------------------
            // Ensure player has 3 cards in hand
            //------------------------------------------------

            while (game.Player.Hand.Count < GameConstants.MAX_HAND_SIZE)
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
                GameUtils.ClearConsoleLine(GameConstants.BOX_THREE_Y_POSITION + index);

                GameUtils.WriteAt(
                    string.Format(GameConstants.CARD_PRINT_FORMAT, index + 1, card.Name),
                    0,
                    GameConstants.BOX_THREE_Y_POSITION + index
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
                GameLogRenderer.ClearLog();
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

                gameStateManager.TransitionTo(new GameMenuState(), game);
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

        /// <summary>
        /// Randomly determines which player takes the first turn.
        /// </summary>
        /// <returns></returns>
        private static EGameTurnState DetermineFirstTurn()
        {
            return (EGameTurnState)GameRandom.Instance.Next(2);
        }

        //------------------------------------------------
        // Round Intro Screen
        //------------------------------------------------

        private void DrawRoundScreen(Game game)
        {
            string introMessage = game.NumberOfRounds > 1 ? string.Format(GameConstants.ROUND_FORMAT, game.CurrentRound) : GameConstants.READY_MESSAGE;
            int introMessageYPosition = GameConstants.BOX_TWO_Y_POSITION + GameConstants.BOX_TWO_WIDTH / 2 - 1;

            ClearGameBoard();

            // Show round number or ready message
            GameUtils.WriteAt(
                introMessage,
                (GameConstants.SPLITTER_TEXT.Length - introMessage.Length) / 2,
                introMessageYPosition
            );

            Thread.Sleep(GameConstants.THREE_SECOND_DELAY);

            GameUtils.ClearConsoleLine(introMessageYPosition);
            GameUtils.WriteAt(
                GameConstants.FIGHT_MESSAGE,
                (GameConstants.SPLITTER_TEXT.Length - GameConstants.FIGHT_MESSAGE.Length) / 2,
                introMessageYPosition
            );

            Thread.Sleep(GameConstants.THREE_SECOND_DELAY);

            GameUtils.ClearConsoleLine(introMessageYPosition);
        }

        //------------------------------------------------
        // Round Result Screen
        //------------------------------------------------

        private void DrawPointScreen(Game game)
        {
            int pointYPosition = GameConstants.BOX_TWO_Y_POSITION + GameConstants.BOX_TWO_WIDTH / 2 - 1;
            string enemyPoints = string.Format(GameConstants.POINT_FORMAT, GameConstants.DEFAULT_ENEMY_NAME, game.Enemy.Wins, game.NumberOfRounds);

            ClearGameBoard();

            GameUtils.WriteAt(
                GameConstants.POINTS_TITLE, 
                (GameConstants.SPLITTER_TEXT.Length - GameConstants.POINTS_TITLE.Length) / 2, 
                GameConstants.BOX_ONE_Y_POSITION
            );

            GameUtils.WriteAt(
                string.Format(GameConstants.POINT_FORMAT, GameConstants.DEFAULT_PLAYER_NAME, game.Player.Wins, game.NumberOfRounds), 
                0, 
                pointYPosition
            );

            GameUtils.WriteAt(
                enemyPoints, 
                GameConstants.SPLITTER_TEXT.Length - enemyPoints.Length,
                pointYPosition
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

            GameUtils.ClearConsoleLine(pointYPosition);

            GameUtils.WriteAt(
                string.Format(GameConstants.POINT_FORMAT, GameConstants.DEFAULT_PLAYER_NAME, game.Player.Wins, game.NumberOfRounds), 
                0,
                pointYPosition
            );

            enemyPoints = string.Format(GameConstants.POINT_FORMAT, GameConstants.DEFAULT_ENEMY_NAME, game.Enemy.Wins, game.NumberOfRounds);

            GameUtils.WriteAt(
                enemyPoints,
                GameConstants.SPLITTER_TEXT.Length - enemyPoints.Length,
                pointYPosition
            );

            Thread.Sleep(GameConstants.THREE_SECOND_DELAY);
        }

        //------------------------------------------------
        // Match Winner Screen
        //------------------------------------------------

        private void DrawPointWinnerScreen(Game game)
        {
            int pointYPosition = GameConstants.BOX_TWO_Y_POSITION + GameConstants.BOX_TWO_WIDTH / 2 - 1;
            string enemyPoints = string.Format(GameConstants.POINT_FORMAT, GameConstants.DEFAULT_ENEMY_NAME, game.Enemy.Wins, game.NumberOfRounds);

            GameUtils.WriteAt(
                GameConstants.POINTS_TITLE,
                (GameConstants.SPLITTER_TEXT.Length - GameConstants.POINTS_TITLE.Length) / 2,
                GameConstants.BOX_ONE_Y_POSITION
            );

            if (game.Player.Wins > game.Enemy.Wins)
            {
                GameUtils.WriteAt(
                    string.Format(GameConstants.WINNER_FORMAT, GameConstants.DEFAULT_PLAYER_NAME), 
                    0,
                    pointYPosition
                );

                GameUtils.WriteAt(
                    enemyPoints,
                    GameConstants.SPLITTER_TEXT.Length - enemyPoints.Length,
                    pointYPosition
                );
            }
            else if (game.Player.Wins < game.Enemy.Wins)
            {
                GameUtils.WriteAt(
                    string.Format(GameConstants.POINT_FORMAT, GameConstants.DEFAULT_PLAYER_NAME, game.Player.Wins, game.NumberOfRounds), 
                    0,
                    pointYPosition
                );

                enemyPoints = string.Format(GameConstants.WINNER_FORMAT, GameConstants.DEFAULT_ENEMY_NAME);

                GameUtils.WriteAt(
                    string.Format(GameConstants.WINNER_FORMAT, GameConstants.DEFAULT_ENEMY_NAME),
                    GameConstants.SPLITTER_TEXT.Length - enemyPoints.Length, 
                    pointYPosition
                );
            }

            GameUtils.WriteAt(
                GameConstants.CONTINUE_MESSAGE, 
                0, 
                GameConstants.BOX_FOUR_Y_POSITION
            );

            Console.ReadKey(true);

            Console.Clear();
        }

        //------------------------------------------------
        // Clear Game Board
        //------------------------------------------------

        private void ClearGameBoard()
        {
            GameUtils.ClearConsoleLine(GameConstants.BOX_ONE_Y_POSITION);

            int maxYPosition = GameConstants.BOX_TWO_Y_POSITION + GameConstants.BOX_TWO_WIDTH;
            for (int i = GameConstants.BOX_TWO_Y_POSITION; i < maxYPosition; i++)
                GameUtils.ClearConsoleLine(i);

            maxYPosition = GameConstants.BOX_THREE_Y_POSITION + GameConstants.BOX_THREE_WIDTH;
            for (int i = GameConstants.BOX_THREE_Y_POSITION; i < maxYPosition; i++)
                GameUtils.ClearConsoleLine(i);

            maxYPosition = GameConstants.BOX_FOUR_Y_POSITION + GameConstants.BOX_FOUR_WIDTH;
            for (int i = GameConstants.BOX_FOUR_Y_POSITION; i < maxYPosition; i++)
                GameUtils.ClearConsoleLine(i);
        }
    }
}
