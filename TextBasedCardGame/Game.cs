using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedCardGame
{
    public class Game
    {
        private int turnNumber = 1;

        private const string splitterText = "==============================";
        private const string cardPrintFormat = "[{0}] {1}";
        private const string heroInfoFormat = "Health = {0}\tAttack = {1}";
        private const string turnInfoFormat = "Turn: {0}";
        private const string enemyActionFormat = "Enemy has played '{0}'";

        // Player Info
        private readonly Deck playerDeck = new Deck();
        private readonly List<Card> playerHand = new List<Card>();
        private int playerHeroHealth = 20;
        private int playerHeroAttack = 1;

        // Enemy Info
        private readonly Deck enemyDeck = new Deck();
        private readonly List<Card> enemyHand = new List<Card>();
        private int enemyHeroHealth = 20;
        private int enemyHeroAttack = 1;
        
        public Game()
        {

        }

        public void StartGame()
        {
            while (true)
            {
                DrawGameBoard();
                HandlePlayerTurn();
                if (enemyHeroHealth <= 0)
                {
                    DrawGameBoard();
                    Console.WriteLine("Congratulations! You Win!");
                    break;
                }

                DrawGameBoard();
                HandleEnemyTurn();
                if (playerHeroHealth <= 0)
                {
                    DrawGameBoard();
                    Console.WriteLine("Sorry... You Lose...");
                    break;
                }
            }
            
        }

        private void DrawGameBoard()
        {
            // Print Turn number
            Console.WriteLine(splitterText);
            Console.WriteLine(string.Format(turnInfoFormat, turnNumber));

            // Print Enemy hero stats
            Console.WriteLine(splitterText);
            Console.WriteLine("\tEnemy Hero:");
            Console.WriteLine(string.Format(heroInfoFormat, enemyHeroHealth, enemyHeroAttack) + "\n\n\n\n");
            
            // Print Player hero stats
            Console.WriteLine("\tPlayer Hero:");
            Console.WriteLine(string.Format(heroInfoFormat, playerHeroHealth, playerHeroAttack));
            Console.WriteLine(splitterText);
        }

        private void HandlePlayerTurn()
        {
            // Draw Player hand
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
            Console.WriteLine(splitterText);

            // Get Player input
            Console.WriteLine("What card do you want to play?");
            Console.WriteLine(splitterText);

            bool successfulInput = false;
            while (!successfulInput)
            {
                try
                {
                    int chosenCardIndex = Convert.ToInt32(Console.ReadLine()) - 1;
                    if (chosenCardIndex >= 0 && chosenCardIndex <= 2)
                    {
                        successfulInput = true;

                        // Activate card's effect
                        switch (playerHand[chosenCardIndex].EffectIndex)
                        {
                            case 0:
                                playerHeroAttack++;
                                break;
                            case 1:
                                playerHeroHealth++;
                                break;
                            case 2:
                                enemyHeroAttack--;
                                break;
                            case 3:
                                enemyHeroHealth--;
                                break;
                        }
                        playerHand.RemoveAt(chosenCardIndex);

                        // Attack the Enemy hero
                        if (playerHeroAttack > 0)
                        {
                            enemyHeroHealth -= playerHeroAttack;
                        }

                        // End of turn
                        Console.WriteLine();
                    }
                    else
                    {
                        // Print message to tell the player they need input a number between 1 and 3
                        // Then reset the player's turn
                        Console.WriteLine("Need a number between 1 and 3");
                    }
                }
                catch (Exception)
                {
                    // Print message to tell the player they need input a number and not a letter
                    // Then reset the player's turn
                    Console.WriteLine("Need a number between 1 and 3");
                }
            }
        }

        private void HandleEnemyTurn()
        {
            // Draw Enemy hand
            while (enemyHand.Count < 3)
            {
                Card card = enemyDeck.DrawCard();
                enemyHand.Add(card);
            }

            // Get card from enemy hand
            switch (enemyHand[0].EffectIndex)
            {
                case 0:
                    enemyHeroAttack++;
                    break;
                case 1:
                    enemyHeroHealth++;
                    break;
                case 2:
                    playerHeroAttack--;
                    break;
                case 3:
                    playerHeroHealth--;
                    break;
            }
            Console.WriteLine(string.Format(enemyActionFormat, enemyHand[0].Name));
            enemyHand.RemoveAt(0);

            // Attack the Player hero
            playerHeroHealth -= enemyHeroAttack;

            // End of turn
            turnNumber++;
            Console.WriteLine();
        }
    }
}
