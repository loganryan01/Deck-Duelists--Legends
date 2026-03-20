using System;
using System.Collections.Generic;
using System.Threading;

namespace TextBasedCardGame
{
    /// <summary>
    /// Text alignment options used when printing text to the console
    /// </summary>
    public enum Alignment
    {
        Left,
        Center,
        Right
    }
    
    /// <summary>
    /// Utility class responsible for drawing and updating the console UI.
    /// All rendering logic for the game board, log, and animations lives here.
    /// </summary>
    public static class GameUtils
    {
        //-----------------------------------------------
        // GAME BOARD
        //-----------------------------------------------

        /// <summary>
        /// Draws the main gameboard including hero stats and turn number.
        /// </summary>
        public static void DrawGameBoard(Player player, Player enemy, int turnNumber)
        {
            // Turn number
            ClearConsoleLine(1);
            WriteAt(string.Format(GameConstants.TURN_INFO_FORMAT, turnNumber), 0, 1);

            //-----------------------------------------------
            // Enemy hero stats
            //-----------------------------------------------

            ClearConsoleLine(3);
            WriteAt(GameConstants.ENEMY_STATS_NAME, 0, 3, Alignment.Center);

            ClearConsoleLine(4);
            WriteAt(string.Format(GameConstants.HERO_HEALTH_FORMAT, enemy.HeroHealth), 0, 4);
            WriteAt(string.Format(GameConstants.HERO_ATTACK_FORMAT, enemy.HeroAttack), 0, 4, Alignment.Right);

            //-----------------------------------------------
            // Player hero stats
            //-----------------------------------------------

            ClearConsoleLine(9);
            WriteAt(GameConstants.PLAYER_STATS_NAME, 0, 9, Alignment.Center);

            ClearConsoleLine(10);
            WriteAt(string.Format(GameConstants.HERO_HEALTH_FORMAT, player.HeroHealth), 0, 10);
            WriteAt(string.Format(GameConstants.HERO_ATTACK_FORMAT, player.HeroAttack), 0, 10, Alignment.Right);
        }

        //-----------------------------------------------
        // LOG SYSTEM
        //-----------------------------------------------

        /// <summary>
        /// Draws the log panel on the right side of the screen.
        /// </summary>
        public static void DrawLog()
        {
            // Draw vertical borders
            for (int i = 0; i < 18; i++)
            {
                WriteAt(GameConstants.LOG_VERTICAL_BORDER, 39, i);
                WriteAt(GameConstants.LOG_VERTICAL_BORDER, 79, i);
            }

            // Log header
            WriteAt(GameConstants.SPLITTER_TEXT, 40, 0);
            WriteAt(GameConstants.LOG_TITLE, 58, 1, Alignment.Center);
            WriteAt(GameConstants.SPLITTER_TEXT, 40, 2);
            WriteAt(GameConstants.SPLITTER_TEXT, 40, 17);
        }

        /// <summary>
        /// Updates the log display with current log entries.
        /// </summary>
        public static void UpdateLog(List<string> log)
        {
            for (int i = 0; i < log.Count; i++)
            {
                WriteAt(log[i], 40, i + 3);
            }
        }

        /// <summary>
        /// Clears the log section.
        /// </summary>
        public static void ClearLog()
        {
            for (int i = 3; i < 17; i++)
            {
                ClearConsoleLine(40, i);
            }
        }

        //-----------------------------------------------
        // TEXT RENDERING
        //-----------------------------------------------

        /// <summary>
        /// Writes text to the console at a specific position with alignment.
        /// </summary>
        public static void WriteAt(string s, int x, int y, Alignment alignment = Alignment.Left)
        {
            int xPos = x;

            if (alignment == Alignment.Center)
            {
                if (x < GameConstants.SPLITTER_TEXT.Length)
                {
                    xPos = (GameConstants.SPLITTER_TEXT.Length - s.Length) / 2;
                }
                else
                {
                    xPos = ((GameConstants.SPLITTER_TEXT.Length * 3) + 2 - s.Length) / 2;
                }
            }
            else if (alignment == Alignment.Right)
            {
                if (x < GameConstants.SPLITTER_TEXT.Length)
                {
                    xPos = GameConstants.SPLITTER_TEXT.Length - s.Length;
                }
                else
                {
                    xPos = (GameConstants.SPLITTER_TEXT.Length * 3) + 2 - s.Length;
                }
            }

            Console.SetCursorPosition(xPos, y);
            Console.Write(s);

            // Move cursor out of the way
            Console.SetCursorPosition(0, y + 1);
        }

        //-----------------------------------------------
        // LINE CLEARING
        //-----------------------------------------------

        /// <summary>
        /// Clears an entire line starting from column 0.
        /// </summary>
        public static void ClearConsoleLine(int y)
        {
            Console.SetCursorPosition(0, y);
            Console.Write(new string(' ', GameConstants.SPLITTER_TEXT.Length));
            Console.SetCursorPosition(0, y);
        }

        /// <summary>
        /// Clears an entire line starting from a given x coordinate.
        /// </summary>
        public static void ClearConsoleLine(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(new string(' ', GameConstants.SPLITTER_TEXT.Length));
            Console.SetCursorPosition(x, y);
        }

        //-----------------------------------------------
        // KO SCREEN
        //-----------------------------------------------

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

                WriteAt(GameConstants.KO_MESSAGE, 16, 6, Alignment.Center);

                // Wait 0.5 seconds
                Thread.Sleep(GameConstants.HALF_SECOND_DELAY);
            }

            ClearConsoleLine(6);
        }
    }
}
