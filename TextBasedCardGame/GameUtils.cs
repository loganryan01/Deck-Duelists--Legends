using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedCardGame
{
    public enum Alignment
    {
        Left,
        Center,
        Right
    }
    
    public static class GameUtils
    {
        public static void DrawGameBoard(Player player, Player enemy, int turnNumber)
        {
            // Print Turn number
            ClearConsoleLine(1);
            WriteAt(string.Format(GameConstants.TURN_INFO_FORMAT, turnNumber), 0, 1);

            // Print Enemy hero stats
            ClearConsoleLine(3);
            WriteAt("Enemy Hero:", 0, 3, Alignment.Center);

            ClearConsoleLine(4);
            WriteAt(string.Format(GameConstants.HERO_HEALTH_FORMAT, enemy.HeroHealth), 0, 4);
            WriteAt(string.Format(GameConstants.HERO_ATTACK_FORMAT, enemy.HeroAttack), 0, 4, Alignment.Right);

            // Print Player hero stats
            ClearConsoleLine(9);
            WriteAt("Player Hero:", 0, 9, Alignment.Center);

            ClearConsoleLine(10);
            WriteAt(string.Format(GameConstants.HERO_HEALTH_FORMAT, player.HeroHealth), 0, 10);
            WriteAt(string.Format(GameConstants.HERO_ATTACK_FORMAT, player.HeroAttack), 0, 10, Alignment.Right);
        }

        public static void DrawLog()
        {
            // Print walls for box
            for (int i = 0; i < 18; i++)
            {
                WriteAt("|", 39, i);
            }

            for (int i = 0; i < 18; i++)
            {
                WriteAt("|", 79, i);
            }

            // Print log title
            WriteAt(GameConstants.SPLITTER_TEXT, 40, 0);
            WriteAt("Log", 58, 1, Alignment.Center);

            // Print the log itself
            WriteAt(GameConstants.SPLITTER_TEXT, 40, 2);
            WriteAt(GameConstants.SPLITTER_TEXT, 40, 17);
        }

        public static void UpdateLog(List<string> log)
        {
            for (int i = 0; i < log.Count; i++)
            {
                WriteAt(log[i], 40, i + 3);
            }
        }

        public static void ClearLog()
        {
            for (int i = 3; i < 17; i++)
            {
                ClearConsoleLine(40, i);
            }
        }

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
            Console.SetCursorPosition(0, y + 1);
        }

        public static void ClearConsoleLine(int y)
        {
            Console.SetCursorPosition(0, y);
            Console.Write(new string(' ', GameConstants.SPLITTER_TEXT.Length));
            Console.SetCursorPosition(0, y);
        }

        public static void ClearConsoleLine(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(new string(' ', GameConstants.SPLITTER_TEXT.Length));
            Console.SetCursorPosition(x, y);
        }

        public static void DrawKOScreen()
        {
            for (int i = 0; i < 7; i++)
            {
                if (i % 2 == 0)
                {
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }

                for (int j = 0; j < 8; j++)
                {
                    Console.SetCursorPosition(0, 3 + j);
                    Console.Write(new string(' ', GameConstants.SPLITTER_TEXT.Length));
                }
                WriteAt("KO!", 16, 6, Alignment.Center);

                // Wait 0.5 seconds before showing the fight screen
                Thread.Sleep(500);
            }

            ClearConsoleLine(6);
        }
    }
}
