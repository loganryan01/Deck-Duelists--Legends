using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedCardGame
{
    public class GameSettingsRoundState : GameState
    {
        public override void DoAction(Game game)
        {
            foreach (var s in GameConstants.TITLE)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine();
            Console.WriteLine(string.Format(GameConstants.GAMES_FORMAT, game.CurrentFormat.ToLower()));

            bool successfulInput = false;
            while (!successfulInput)
            {
                try
                {
                    Console.CursorVisible = true;
                    int playerInput = Convert.ToInt32(Console.ReadLine());

                    if (playerInput > 0)
                    {
                        Console.CursorVisible = false;
                        successfulInput = true;

                        Console.Clear();
                        game.SetNumberOfRounds(playerInput);
                        gameStateManager.TransitionTo(new GameSettingsState());
                    }
                    else
                    {
                        Console.WriteLine(GameConstants.INVALID_INPUT_MESSAGE);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine(GameConstants.INVALID_INPUT_MESSAGE);
                }
            }
        }
    }
}
