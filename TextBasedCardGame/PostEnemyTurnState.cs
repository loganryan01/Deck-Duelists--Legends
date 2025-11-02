using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedCardGame
{
    public class PostEnemyTurnState : GameTurnState
    {
        public override void DoAction(Game game)
        {
            if (game.Player.HeroHealth <= 0)
            {
                GameUtils.DrawGameBoard(game.Player, game.Enemy, game.TurnNumber);
                Console.WriteLine("Sorry... You Lose...");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
                game.StopGame();
            }
            else
            {
                gameTurnStateManager.TransitionTo(new PrePlayerTurnState());
            }
        }
    }
}
