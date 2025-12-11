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
            Console.WriteLine("\tEnemy Hero:");
            Console.WriteLine(string.Format(GameConstants.HERO_INFO_FORMAT, enemy.HeroHealth, enemy.HeroAttack) + "\n\n\n\n");

            // Print Player hero stats
            Console.WriteLine("\tPlayer Hero:");
            Console.WriteLine(string.Format(GameConstants.HERO_INFO_FORMAT, player.HeroHealth, player.HeroAttack));
            Console.WriteLine(GameConstants.SPLITTER_TEXT);
        }
    }
}
