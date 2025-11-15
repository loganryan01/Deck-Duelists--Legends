using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedCardGame
{
    public class PreEnemyTurnState : GameTurnState
    {
        public override void DoAction(Game game)
        {
            if (game.Enemy.Deck.Cards.Count == 0)
            {
                if (game.IsSim)
                {
                    Console.WriteLine(string.Format(GameConstants.PLAYER_WINS_SIM_FORMAT, (game.SimGameNumber + 1).ToString(), game.TurnNumber.ToString()));
                    game.Player.IncrementWinNumber();
                }
                else
                {
                    GameUtils.DrawGameBoard(game.Player, game.Enemy, game.TurnNumber);
                    Console.WriteLine("Congratulations! You Win!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                }
                    
                game.StopGame();
            }
            else
            {
                gameTurnStateManager.TransitionTo(new EnemyTurnState());
            }
        }
    }
}
