using System;
using System.Collections.Generic;
using System.Threading;

namespace TextBasedCardGame
{
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
            string enemyHeroAttack = string.Format(GameConstants.HERO_ATTACK_FORMAT, enemy.HeroAttack);
            string playerHeroAttack = string.Format(GameConstants.HERO_ATTACK_FORMAT, player.HeroAttack);

            // Turn number
            ClearConsoleLine(GameConstants.BOX_ONE_Y_POSITION);
            WriteAt(string.Format(GameConstants.TURN_INFO_FORMAT, turnNumber), 0, GameConstants.BOX_ONE_Y_POSITION);

            //-----------------------------------------------
            // Enemy hero stats
            //-----------------------------------------------

            ClearConsoleLine(GameConstants.BOX_TWO_Y_POSITION);
            WriteAt(
                GameConstants.ENEMY_STATS_NAME, 
                (GameConstants.SPLITTER_TEXT.Length - GameConstants.ENEMY_STATS_NAME.Length) / 2, 
                GameConstants.BOX_TWO_Y_POSITION
            );

            ClearConsoleLine(GameConstants.BOX_TWO_Y_POSITION + 1);
            WriteAt(string.Format(GameConstants.HERO_HEALTH_FORMAT, enemy.HeroHealth), 0, GameConstants.BOX_TWO_Y_POSITION + 1);
            WriteAt(
                enemyHeroAttack,
                GameConstants.SPLITTER_TEXT.Length - enemyHeroAttack.Length, 
                GameConstants.BOX_TWO_Y_POSITION + 1
            );

            //-----------------------------------------------
            // Player hero stats
            //-----------------------------------------------

            ClearConsoleLine(GameConstants.BOX_TWO_Y_POSITION + GameConstants.BOX_TWO_WIDTH - 2);
            WriteAt(
                GameConstants.PLAYER_STATS_NAME,
                (GameConstants.SPLITTER_TEXT.Length - GameConstants.PLAYER_STATS_NAME.Length) / 2, 
                GameConstants.BOX_TWO_Y_POSITION + GameConstants.BOX_TWO_WIDTH - 2
            );

            ClearConsoleLine(GameConstants.BOX_TWO_Y_POSITION + GameConstants.BOX_TWO_WIDTH - 1);
            WriteAt(string.Format(GameConstants.HERO_HEALTH_FORMAT, player.HeroHealth), 0, GameConstants.BOX_TWO_Y_POSITION + GameConstants.BOX_TWO_WIDTH - 1);
            WriteAt(
                playerHeroAttack,
                GameConstants.SPLITTER_TEXT.Length - playerHeroAttack.Length, 
                GameConstants.BOX_TWO_Y_POSITION + GameConstants.BOX_TWO_WIDTH - 1
            );
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
            for (int i = 0; i < GameConstants.LOG_WIDTH; i++)
            {
                WriteAt(
                    GameConstants.LOG_VERTICAL_BORDER, 
                    GameConstants.SPLITTER_TEXT.Length, 
                    i
                );

                WriteAt(
                    GameConstants.LOG_VERTICAL_BORDER,
                    (GameConstants.SPLITTER_TEXT.Length * 2) + GameConstants.LOG_VERTICAL_BORDER.Length, 
                    i
                );
            }

            // Log header
            WriteAt(
                GameConstants.SPLITTER_TEXT,
                GameConstants.SPLITTER_TEXT.Length + GameConstants.LOG_VERTICAL_BORDER.Length, 
                0
            );

            WriteAt(
                GameConstants.LOG_TITLE,
                GameConstants.SPLITTER_TEXT.Length + GameConstants.LOG_VERTICAL_BORDER.Length + ((GameConstants.SPLITTER_TEXT.Length / 2) - GameConstants.LOG_TITLE.Length / 2), 
                GameConstants.LOG_BOX_ONE_Y_POSITION
            );

            WriteAt(
                GameConstants.SPLITTER_TEXT,
                GameConstants.SPLITTER_TEXT.Length + GameConstants.LOG_VERTICAL_BORDER.Length,
                GameConstants.LOG_BOX_ONE_Y_POSITION + GameConstants.LOG_BOX_ONE_WIDTH
            );

            WriteAt(
                GameConstants.SPLITTER_TEXT,
                GameConstants.SPLITTER_TEXT.Length + GameConstants.LOG_VERTICAL_BORDER.Length, 
                GameConstants.LOG_BOX_TWO_Y_POSITION + GameConstants.MAX_LOG_SIZE + 1
            );
        }

        /// <summary>
        /// Updates the log display with current log entries.
        /// </summary>
        public static void UpdateLog(List<string> log)
        {
            for (int i = 0; i < log.Count; i++)
            {
                ClearLogLine(i + GameConstants.LOG_BOX_TWO_Y_POSITION);
                WriteAt(log[i], GameConstants.SPLITTER_TEXT.Length + GameConstants.LOG_VERTICAL_BORDER.Length, i + GameConstants.LOG_BOX_TWO_Y_POSITION);
            }
        }

        /// <summary>
        /// Clears the log section.
        /// </summary>
        public static void ClearLog()
        {
            for (int i = GameConstants.LOG_BOX_TWO_Y_POSITION; i < GameConstants.LOG_WIDTH - 1; i++)
            {
                ClearLogLine(i);
            }
        }

        //-----------------------------------------------
        // TEXT RENDERING
        //-----------------------------------------------

        /// <summary>
        /// Writes text to the console at a specific position.
        /// </summary>
        public static void WriteAt(string s, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(s);
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
        /// Clears an entire line starting from log starting position.
        /// </summary>
        public static void ClearLogLine(int y)
        {
            Console.SetCursorPosition(GameConstants.SPLITTER_TEXT.Length + GameConstants.LOG_VERTICAL_BORDER.Length, y);
            Console.Write(new string(' ', GameConstants.SPLITTER_TEXT.Length));
            Console.SetCursorPosition(GameConstants.SPLITTER_TEXT.Length + GameConstants.LOG_VERTICAL_BORDER.Length, y);
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

                WriteAt(
                    GameConstants.KO_MESSAGE, 
                    (GameConstants.SPLITTER_TEXT.Length - GameConstants.KO_MESSAGE.Length) / 2, 
                    GameConstants.BOX_TWO_Y_POSITION + (GameConstants.BOX_TWO_WIDTH / 2) - 1
                );

                // Wait 0.5 seconds
                Thread.Sleep(GameConstants.HALF_SECOND_DELAY);
            }

            ClearConsoleLine(GameConstants.BOX_TWO_Y_POSITION + (GameConstants.BOX_TWO_WIDTH / 2) - 1);
        }
    }
}
