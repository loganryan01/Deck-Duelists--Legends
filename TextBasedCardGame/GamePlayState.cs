using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedCardGame
{
    public class GamePlayState : GameState
    {
        public override void DoAction(Game game)
        {
            EGameTurnState gameTurnState = DetermineFirstTurn();
            GameTurnState gameTurnStrategy = gameTurnState == EGameTurnState.Player ? new PrePlayerTurnState() : new PreEnemyTurnState();
            GameTurnStateManager gameTurnStateManager = new GameTurnStateManager(gameTurnStrategy);

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

            DrawRoundScreen(game);

            // Draw Player hand
            while (game.Player.Hand.Count < 3)
            {
                Card card = game.Player.Deck.DrawCard();
                game.Player.Hand.Add(card);
            }

            // Print Player hand
            int i = 0;
            foreach (Card card in game.Player.Hand)
            {
                GameUtils.ClearConsoleLine(12 + i);
                GameUtils.WriteAt(string.Format(GameConstants.CARD_PRINT_FORMAT, i + 1, card.Name), 0, 12 + i);
                i++;
            }

            while (game.IsPlaying)
            {
                gameTurnStateManager.DoAction(game);
            }

            if (game.IsLogEnabled)
            {
                GameUtils.ClearLog();
            }

            DrawPointScreen(game);

            game.ResetGame();

            if (game.CurrentFormat == GameConstants.GAME_FORMATS[0] && game.CurrentRound < game.NumberOfRounds || 
                game.CurrentFormat == GameConstants.GAME_FORMATS[1] && game.Player.Wins < game.NumberOfRounds && game.Enemy.Wins < game.NumberOfRounds)
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

        private static EGameTurnState DetermineFirstTurn()
        {
            Random random = new Random();
            int playerNumber = random.Next(2);
            return (EGameTurnState)playerNumber;
        }

        private void DrawRoundScreen(Game game)
        {
            GameUtils.ClearConsoleLine(1);

            for (int i = 3; i < 11; i++)
            {
                GameUtils.ClearConsoleLine(i);
            }

            for (int j = 12; j < 15; j++)
            {
                GameUtils.ClearConsoleLine(j);
            }

            for (int k = 16; k < 18; k++)
            {
                GameUtils.ClearConsoleLine(k);
            }

            // Draw "Round x" screen
            if (game.NumberOfRounds > 1)
            {
                GameUtils.WriteAt(string.Format(GameConstants.ROUND_FORMAT, game.CurrentRound), 16, 6, Alignment.Center);
            }
            else
            {
                GameUtils.WriteAt("Ready", 16, 6, Alignment.Center);
            }

            // Wait 3 seconds before showing the fight screen
            Thread.Sleep(3000);

            GameUtils.ClearConsoleLine(6);
            GameUtils.WriteAt("FIGHT!", 16, 6, Alignment.Center);

            // Wait 3 seconds before starting game
            Thread.Sleep(3000);

            GameUtils.ClearConsoleLine(6);
        }

        private void DrawPointScreen(Game game)
        {
            GameUtils.ClearConsoleLine(1);

            for (int i = 3; i < 11; i++)
            {
                GameUtils.ClearConsoleLine(i);
            }

            for (int j = 12; j < 15; j++)
            {
                GameUtils.ClearConsoleLine(j);
            }

            for (int k = 16; k < 18; k++)
            {
                GameUtils.ClearConsoleLine(k);
            }

            GameUtils.WriteAt("Points", 16, 1, Alignment.Center);
            GameUtils.WriteAt(string.Format(GameConstants.POINT_FORMAT, "Player", game.Player.Wins, game.NumberOfRounds), 0, 6);
            GameUtils.WriteAt(string.Format(GameConstants.POINT_FORMAT, "Enemy", game.Enemy.Wins, game.NumberOfRounds), 0, 6, Alignment.Right);

            if (game.Player.Deck.Cards.Count == 0 || game.Player.HeroHealth <= 0)
            {
                // Enemy wins
                game.Enemy.IncrementWinNumber();
            }
            else if (game.Enemy.Deck.Cards.Count == 0 || game.Enemy.HeroHealth <= 0)
            {
                // Player wins
                game.Player.IncrementWinNumber();
            }

            Thread.Sleep(3000);

            GameUtils.ClearConsoleLine(6);
            GameUtils.WriteAt(string.Format(GameConstants.POINT_FORMAT, "Player", game.Player.Wins, game.NumberOfRounds), 0, 6);
            GameUtils.WriteAt(string.Format(GameConstants.POINT_FORMAT, "Enemy", game.Enemy.Wins, game.NumberOfRounds), 0, 6, Alignment.Right);

            Thread.Sleep(3000);
        }

        private void DrawPointWinnerScreen(Game game)
        {
            GameUtils.WriteAt("Points", 16, 1, Alignment.Center);

            if (game.Player.Wins > game.Enemy.Wins)
            {
                GameUtils.WriteAt("Player = Winner!", 0, 6);
                GameUtils.WriteAt(string.Format(GameConstants.POINT_FORMAT, "Enemy", game.Enemy.Wins, game.NumberOfRounds), 0, 6, Alignment.Right);
            }
            else
            {
                GameUtils.WriteAt(string.Format(GameConstants.POINT_FORMAT, "Player", game.Player.Wins, game.NumberOfRounds), 0, 6);
                GameUtils.WriteAt("Enemy = Winner!", 0, 6, Alignment.Right);
            }

            Console.SetCursorPosition(0, 16);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            Console.Clear();
        }
    }
}
