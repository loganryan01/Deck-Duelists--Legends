using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedCardGame
{
    public class PostPlayerTurnState : GameTurnState
    {
        public override void DoAction(Game game)
        {
            if (game.Enemy.HeroHealth <= 0)
            {
                GameUtils.DrawGameBoard(game.Player, game.Enemy, game.TurnNumber);
                if (game.IsLogEnabled)
                {
                    GameUtils.DrawLog(game.Log);
                }
                Console.WriteLine("Congratulations! You Win!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);

                game.StopGame();
            }
            else
            {
                gameTurnStateManager.TransitionTo(new PreEnemyTurnState());
            }
        }
    }
}
