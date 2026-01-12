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

            // Draw "Round x" screen
            Console.Clear();
            Console.WriteLine(GameConstants.SPLITTER_TEXT + "\n");
            Console.WriteLine(GameConstants.SPLITTER_TEXT + "\n\n\n\n\n\n\n\n");
            Console.WriteLine(GameConstants.SPLITTER_TEXT + "\n\n\n");
            Console.WriteLine(GameConstants.SPLITTER_TEXT);

            GameUtils.WriteAt(string.Format(GameConstants.ROUND_FORMAT, game.CurrentRound), 16, 6, Alignment.Center);

            // Wait 3 seconds before showing the fight screen
            Thread.Sleep(3000);

            GameUtils.ClearConsoleLine(6);
            GameUtils.WriteAt("FIGHT!", 16, 6, Alignment.Center);

            // Wait 3 seconds before starting game
            Thread.Sleep(3000);

            while (game.IsPlaying)
            {
                gameTurnStateManager.DoAction(game);
            }

            Console.Clear();
            game.ResetGame();

            if (game.CurrentRound < game.NumberOfRounds)
            {
                game.IncrementCurrentRound();
            }
            else
            {
                gameStateManager.TransitionTo(new GameMenuState());
            }
        }

        private static EGameTurnState DetermineFirstTurn()
        {
            Random random = new Random();
            int playerNumber = random.Next(2);
            return (EGameTurnState)playerNumber;
        }
    }
}
