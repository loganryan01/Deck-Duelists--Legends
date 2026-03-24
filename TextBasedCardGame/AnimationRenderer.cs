using System;
using System.Threading;

namespace TextBasedCardGame
{
    /// <summary>
    /// Responsible for rendering all the animations in the game.
    /// </summary>
    public static class AnimationRenderer
    {
        /// <summary>
        /// Displays a flashing KO animation when a hero is defeated.
        /// </summary>
        public static void DrawKOScreen()
        {
            for (int i = 0; i < GameConstants.NUMBER_OF_FLASHES; i++)
            {
                // Flash colors
                if (i % 2 == 0)
                {
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }

                // Draw flash block
                for (int j = 0; j < 8; j++)
                {
                    Console.SetCursorPosition(0, 3 + j);
                    Console.Write(new string(' ', GameConstants.SPLITTER_TEXT.Length));
                }

                GameUtils.WriteAt(
                    GameConstants.KO_MESSAGE,
                    (GameConstants.SPLITTER_TEXT.Length - GameConstants.KO_MESSAGE.Length) / 2,
                    GameConstants.BOX_TWO_Y_POSITION + (GameConstants.BOX_TWO_WIDTH / 2) - 1
                );

                // Wait 0.5 seconds
                Thread.Sleep(GameConstants.HALF_SECOND_DELAY);
            }

            GameUtils.ClearConsoleLine(GameConstants.BOX_TWO_Y_POSITION + (GameConstants.BOX_TWO_WIDTH / 2) - 1);
        }
    }
}
