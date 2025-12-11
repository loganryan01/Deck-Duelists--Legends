using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedCardGame
{
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
            Console.WriteLine("\t   Enemy Hero:");
            Console.WriteLine(string.Format(GameConstants.HERO_INFO_FORMAT, enemy.HeroHealth, enemy.HeroAttack) + "\n\n\n\n");

            // Print Player hero stats
            Console.WriteLine("\t   Player Hero:");
            Console.WriteLine(string.Format(GameConstants.HERO_INFO_FORMAT, player.HeroHealth, player.HeroAttack));
            Console.WriteLine(GameConstants.SPLITTER_TEXT);

            // Set cursor position underneath game board when done
            Console.SetCursorPosition(0, 12);
        }

        public static void DrawLog(List<string> log)
        {
            // Print walls for box
            for (int i = 0; i < 18; i++)
            {
                WriteAt("|", 34, i);
            }

            for (int i = 0; i < 18; i++)
            {
                WriteAt("|", 69, i);
            }

            // Print log title
            WriteAt(GameConstants.SPLITTER_TEXT, 35, 0);
            WriteAt("Log", 50, 1);

            // Print the log itself
            WriteAt(GameConstants.SPLITTER_TEXT, 35, 2);
            for (int i = 0; i < log.Count; i++)
            {
                WriteAt(log[i], 35, i + 3);
            }
            WriteAt(GameConstants.SPLITTER_TEXT, 35, 17);

            // Set cursor position underneath game board when done
            Console.SetCursorPosition(0, 12);
        }

        public static void WriteAt(string s, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(s);
        }
    }
}
