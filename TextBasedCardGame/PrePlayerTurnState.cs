using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedCardGame
{
    public class PrePlayerTurnState : GameTurnState
    {
        public override void DoAction(Game game)
        {
            if (game.Player.Deck.Cards.Count == 0)
            {
                if (game.IsLogEnabled)
                {
                    GameUtils.UpdateLog(game.Log);
                }
                GameUtils.DrawKOScreen();
                GameUtils.DrawGameBoard(game.Player, game.Enemy, game.TurnNumber);
                GameUtils.ClearConsoleLine(16);
                GameUtils.WriteAt("Sorry... You Lose...", 0, 16);
                GameUtils.ClearConsoleLine(17);
                GameUtils.WriteAt("Press any key to continue...", 0, 17);
                Console.ReadKey(true);

                game.StopGame();
            }
            else
            {
                gameTurnStateManager.TransitionTo(new PlayerTurnState());
            }
        }
    }
}