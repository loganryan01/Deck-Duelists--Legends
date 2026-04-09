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
            GameUtils.WriteAt(
                string.Format(GameConstants.GAMES_FORMAT, game.CurrentFormat.ToLower()), 
                0, 
                GameConstants.TITLE.Length + 1
            );
            Console.SetCursorPosition(0, GameConstants.TITLE.Length + 2);

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

                        GameUtils.ClearConsoleLine(GameConstants.TITLE.Length + 1);
                        GameUtils.ClearConsoleLine(GameConstants.TITLE.Length + 2);
                        GameUtils.ClearConsoleLine(GameConstants.TITLE.Length + 3);

                        game.SetNumberOfRounds(playerInput);
                        gameStateManager.TransitionTo(new GameSettingsState(), game);
                    }
                    else
                    {
                        GameUtils.WriteAt(GameConstants.INVALID_INPUT_MESSAGE, 0, GameConstants.TITLE.Length + 2);
                        GameUtils.ClearConsoleLine(GameConstants.TITLE.Length + 3);
                        Console.SetCursorPosition(0, GameConstants.TITLE.Length + 3);
                    }
                }
                catch (Exception)
                {
                    GameUtils.WriteAt(GameConstants.INVALID_INPUT_MESSAGE, 0, GameConstants.TITLE.Length + 2);
                    GameUtils.ClearConsoleLine(GameConstants.TITLE.Length + 3);
                    Console.SetCursorPosition(0, GameConstants.TITLE.Length + 3);
                }
            }
        }

        public override void Enter(Game game)
        {
            // Empty
        }

        public override void Exit(Game game)
        {
            // Empty
        }
    }
}
