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

            while (game.IsPlaying)
            {
                gameTurnStateManager.DoAction(game);
            }

            Console.Clear();
            game.ResetGame();
            gameStateManager.TransitionTo(new GameMenuState());
        }

        private static EGameTurnState DetermineFirstTurn()
        {
            Random random = new Random();
            int playerNumber = random.Next(2);
            return (EGameTurnState)playerNumber;
        }
    }
}
