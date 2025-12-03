using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedCardGame
{
    public class GameMenuState : GameState
    {
        private readonly string[] TITLE = { " _____        _       ___                  _      ___             _      ___                ",
                                            "|_   _|____ _| |_ ___| _ ) __ _ ___ ___ __| |___ / __|__ _ _ _ __| |___ / __|__ _ _ __  ___ ",
                                            "  | |/ -_) \\ /  _|___| _ \\/ _` (_-</ -_) _` |___| (__/ _` | '_/ _` |___| (_ / _` | '  \\/ -_)",
                                            "  |_|\\___/_\\_\\\\__|   |___/\\__,_/__/\\___\\__,_|    \\___\\__,_|_| \\__,_|    \\___\\__,_|_|_|_\\___|"};
        
        public override void DoAction(Game game)
        {
            foreach (var s in TITLE)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine();
            Console.WriteLine(string.Format(GameConstants.CARD_PRINT_FORMAT, "1", "Play"));
            Console.WriteLine(string.Format(GameConstants.CARD_PRINT_FORMAT, "2", "Exit"));

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
                        gameStateManager.TransitionTo(new GamePlayState());
                    }
                    else if (playerInput == 2)
                    {
                        successfulInput = true;

                        game.CloseGame();
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
