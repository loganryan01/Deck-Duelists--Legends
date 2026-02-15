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
            // Clear console
            Console.Clear();
            
            // Print Turn number
            Console.WriteLine(GameConstants.SPLITTER_TEXT);
            Console.WriteLine(string.Format(GameConstants.TURN_INFO_FORMAT, turnNumber));

            // Print Enemy hero stats
            Console.WriteLine(GameConstants.SPLITTER_TEXT);
            WriteAt("Enemy Hero:", 0, 3, Alignment.Center);
            WriteAt(string.Format(GameConstants.HERO_HEALTH_FORMAT, enemy.HeroHealth), 0, 4);
            WriteAt(string.Format(GameConstants.HERO_ATTACK_FORMAT, enemy.HeroAttack), 0, 4, Alignment.Right);

            // Print Player hero stats
            WriteAt("Player Hero:", 0, 9, Alignment.Center);
            WriteAt(string.Format(GameConstants.HERO_HEALTH_FORMAT, player.HeroHealth), 0, 10);
            WriteAt(string.Format(GameConstants.HERO_ATTACK_FORMAT, player.HeroAttack), 0, 10, Alignment.Right);
            Console.WriteLine(GameConstants.SPLITTER_TEXT);

            // Set cursor position underneath game board when done
            Console.SetCursorPosition(0, 12);
        }

        public static void DrawLog(List<string> log)
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
            for (int i = 0; i < log.Count; i++)
            {
                WriteAt(log[i], 40, i + 3);
            }
            WriteAt(GameConstants.SPLITTER_TEXT, 40, 17);

            // Set cursor position underneath game board when done
            Console.SetCursorPosition(0, 12);
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
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, y);
        }
    }
}
