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

        private const string cardPrintFormat = "[{0}] {1}";
        private const string heroInfoFormat = "Health = {0}\tAttack = {1}";

        // Player Info
        private Deck playerDeck = new Deck();
        private List<Card> playerHand = new List<Card>();
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
            }
        }

        public void DrawGameBoard()
        {
            // Print Enemy hero stats
            Console.WriteLine("==============================");
            Console.WriteLine("\tEnemy Hero:");
            Console.WriteLine(string.Format(heroInfoFormat, enemyHeroHealth, enemyHeroAttack) + "\n\n\n\n");
            
            // Print Player hero stats
            Console.WriteLine("\tPlayer Hero:");
            Console.WriteLine(string.Format(heroInfoFormat, playerHeroHealth, playerHeroAttack));
            Console.WriteLine("==============================");

            // Print Player hand
            for (int i = 0; i < 3; i++)
            {
                Card card = playerDeck.DrawCard();
                Console.WriteLine(string.Format(cardPrintFormat, i + 1, card.Name));
                playerHand.Add(card);
            }
            Console.WriteLine("==============================");

            // Get Player input
            Console.WriteLine("What card do you want to play?");
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
