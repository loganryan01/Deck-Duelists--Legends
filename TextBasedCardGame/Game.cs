using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedCardGame
{
    public enum GameState
    {
        MainMenu,
        PlayGame,
        PlayerVictory,
        PlayerDefeat
    }
    
    public class Game
    {
        private readonly GameState state = GameState.PlayGame;
        private int turnNumber = 1;

        private const string cardPrintFormat = "[{0}] {1}";
        private const string heroInfoFormat = "Health = {0}\tAttack = {1}";
        private const string turnInfoFormat = "Turn: {0}";

        // Player Info
        private readonly Deck playerDeck = new Deck();
        private readonly List<Card> playerHand = new List<Card>();
        private int playerHeroHealth = 20;
        private int playerHeroAttack = 1;

        // Enemy Info
        private int enemyHeroHealth = 20;
        private int enemyHeroAttack = 1;
        
        public Game()
        {

        }

        public void StartGame()
        {
            if (state == GameState.PlayGame)
            {
                // Draw
                DrawGameBoard();
                DrawGameBoard();
            }
        }

        public void DrawGameBoard()
        {
            // Print Turn number
            Console.WriteLine("==============================");
            Console.WriteLine(string.Format(turnInfoFormat, turnNumber));

            // Print Enemy hero stats
            Console.WriteLine("==============================");
            Console.WriteLine("\tEnemy Hero:");
            Console.WriteLine(string.Format(heroInfoFormat, enemyHeroHealth, enemyHeroAttack) + "\n\n\n\n");
            
            // Print Player hero stats
            Console.WriteLine("\tPlayer Hero:");
            Console.WriteLine(string.Format(heroInfoFormat, playerHeroHealth, playerHeroAttack));
            Console.WriteLine("==============================");
            
            // Draw player hand
            while (playerHand.Count < 3)
            {
                Card card = playerDeck.DrawCard();
                playerHand.Add(card);
            }

            // Print Player hand
            int i = 0;
            foreach (Card card in playerHand)
            {
                Console.WriteLine(string.Format(cardPrintFormat, i + 1, card.Name));
                i++;
            }
            Console.WriteLine("==============================");

            // Get Player input
            Console.WriteLine("What card do you want to play?");
            Console.WriteLine("==============================");
            try
            {
                int chosenCardIndex = Convert.ToInt32(Console.ReadLine()) - 1;
                if (chosenCardIndex >= 0 && chosenCardIndex <= 2)
                {
                    // Activate card's effect
                    if (playerHand[chosenCardIndex].EffectIndex == 0) 
                    {
                        // Assume effectIndex 0 is to increase hero's attack by 1
                        playerHeroAttack++;
                    }
                    playerHand.RemoveAt(chosenCardIndex);

                    // Attack the Enemy hero
                    enemyHeroHealth -= playerHeroAttack;

                    // End of turn
                    turnNumber++;
                }
                else
                {
                    // Print message to tell the player they need input a number between 1 and 3
                    // Then reset the player's turn
                }
            }
            catch (Exception)
            {
                // Print message to tell the player they need input a number and not a letter
                // Then reset the player's turn
                Console.WriteLine("Need a number");
            }
            
        }
    }
}
