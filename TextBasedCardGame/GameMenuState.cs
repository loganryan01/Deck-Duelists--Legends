using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedCardGame
{
    public class GameMenuState : GameState
    {
        public override void DoAction(Game game)
        {
            foreach (var s in GameConstants.TITLE)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine();
            Console.WriteLine(string.Format(GameConstants.CARD_PRINT_FORMAT, "1", "Play"));
            Console.WriteLine(string.Format(GameConstants.CARD_PRINT_FORMAT, "2", "Settings"));
            Console.WriteLine(string.Format(GameConstants.CARD_PRINT_FORMAT, "3", "Exit"));

            bool successfulInput = false;
            while (!successfulInput)
            {
                try
                {
                    Console.CursorVisible = true;
                    int playerInput = Convert.ToInt32(Console.ReadLine());

                    if (playerInput == 1)
                    {
                        Console.CursorVisible = false;
                        successfulInput = true;

                        Console.Clear();
                        gameStateManager.TransitionTo(new GamePlayState());
                    }
                    else if (playerInput == 2)
                    {
                        Console.CursorVisible = false;
                        successfulInput = true;

                        Console.Clear();
                        gameStateManager.TransitionTo(new GameSettingsState());
                    }
                    else if (playerInput == 3)
                    {
                        Console.CursorVisible = false;
                        successfulInput = true;

                        game.CloseGame();
                    }
                    else
                    {
                        Console.WriteLine(GameConstants.INVAILD_INPUT_MESSAGE);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine(GameConstants.INVAILD_INPUT_MESSAGE);
                }
            }
        }
    }
}
