using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedCardGame
{
    public class GameSettingsState : GameState
    {
        public override void DoAction(Game game)
        {
            foreach (var s in GameConstants.TITLE)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine();
            Console.WriteLine(string.Format(GameConstants.CARD_PRINT_FORMAT, "1", string.Format("Log = {0}", game.IsLogEnabled)));
            Console.WriteLine(string.Format(GameConstants.CARD_PRINT_FORMAT, "2", "Back"));

            bool successfulInput = false;
            while (!successfulInput)
            {
                try
                {
                    int playerInput = Convert.ToInt32(Console.ReadLine());

                    if (playerInput == 1)
                    {
                        successfulInput = true;

                        Console.Clear();
                        game.EnableLog(!game.IsLogEnabled);
                    }
                    else if (playerInput == 2)
                    {
                        successfulInput = true;

                        Console.Clear();
                        gameStateManager.TransitionTo(new GameMenuState());
                    }
                    else
                    {
                        Console.WriteLine("Need a valid number");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Need a valid number");
                }
            }
        }
    }
}
