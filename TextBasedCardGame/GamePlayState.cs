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

            DrawRoundScreen(game);

            while (game.IsPlaying)
            {
                gameTurnStateManager.DoAction(game);
            }

            DrawPointScreen(game);

            Console.Clear();
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
            // Draw "Round x" screen
            Console.Clear();
            Console.WriteLine(GameConstants.SPLITTER_TEXT + "\n");
            Console.WriteLine(GameConstants.SPLITTER_TEXT + "\n\n\n\n\n\n\n\n");
            Console.WriteLine(GameConstants.SPLITTER_TEXT + "\n\n\n");
            Console.WriteLine(GameConstants.SPLITTER_TEXT);

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
        }

        private void DrawPointScreen(Game game)
        {
            Console.Clear();
            Console.WriteLine(GameConstants.SPLITTER_TEXT + "\n");
            Console.WriteLine(GameConstants.SPLITTER_TEXT + "\n\n\n\n\n\n\n\n");
            Console.WriteLine(GameConstants.SPLITTER_TEXT + "\n\n\n");
            Console.WriteLine(GameConstants.SPLITTER_TEXT);

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
            Console.WriteLine(GameConstants.SPLITTER_TEXT + "\n");
            Console.WriteLine(GameConstants.SPLITTER_TEXT + "\n\n\n\n\n\n\n\n");
            Console.WriteLine(GameConstants.SPLITTER_TEXT + "\n\n\n");
            Console.WriteLine(GameConstants.SPLITTER_TEXT);

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
