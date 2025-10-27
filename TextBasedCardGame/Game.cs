using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedCardGame
{
    public class Game
    {
        private const int STARTING_TURN_NUMBER = 1;
        private const int STARTING_HERO_HEALTH = 20;
        private const int STARTING_HERO_ATTACK = 1;

        private const string SPLITTER_TEXT = "==============================";
        private const string CARD_PRINT_FORMAT = "[{0}] {1}";
        private const string HERO_INFO_FORMAT = "Health = {0}\tAttack = {1}";
        private const string TURN_INFO_FORMAT = "Turn: {0}";
        private const string ENEMY_ACTION_FORMAT = "Enemy has played '{0}'";
        private const string PLAYER_WINS_SIM_FORMAT = "Player wins Game {0} by Turn {1}";
        private const string ENEMY_WINS_SIM_FORMAT = "Enemy wins Game {0} by Turn {1}";

        private int turnNumber = STARTING_TURN_NUMBER;

        // Player Info
        private Deck playerDeck = new Deck();
        private readonly List<Card> playerHand = new List<Card>();
        private int playerHeroHealth = STARTING_HERO_HEALTH;
        private int playerHeroAttack = STARTING_HERO_ATTACK;

        // Enemy Info
        private Deck enemyDeck = new Deck();
        private readonly List<Card> enemyHand = new List<Card>();
        private int enemyHeroHealth = STARTING_HERO_HEALTH;
        private int enemyHeroAttack = STARTING_HERO_ATTACK;
        
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
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                    break;
                }

                DrawGameBoard();
                HandleEnemyTurn();
                if (playerHeroHealth <= 0)
                {
                    DrawGameBoard();
                    Console.WriteLine("Sorry... You Lose...");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                    break;
                }
            }
            
        }

        public void StartGameSim()
        {
            int playerWins = 0;
            int enemyWins = 0;

            for (int i = 0; i < 100; i++)
            {
                while (true)
                {
                    HandlePlayerTurnSim();
                    if (enemyHeroHealth <= 0)
                    {
                        Console.WriteLine(string.Format(PLAYER_WINS_SIM_FORMAT, (i + 1).ToString(), turnNumber.ToString()));
                        playerWins++;
                        break;
                    }
                    if (playerDeck.Cards.Count == 0)
                    {
                        Console.WriteLine(string.Format(ENEMY_WINS_SIM_FORMAT, (i + 1).ToString(), turnNumber.ToString()));
                        enemyWins++;
                        break;
                    }

                    HandleEnemyTurnSim();
                    if (playerHeroHealth <= 0)
                    {
                        Console.WriteLine(string.Format(ENEMY_WINS_SIM_FORMAT, (i + 1).ToString(), turnNumber.ToString()));
                        enemyWins++;
                        break;
                    }
                    if (enemyDeck.Cards.Count == 0)
                    {
                        Console.WriteLine(string.Format(PLAYER_WINS_SIM_FORMAT, (i + 1).ToString(), turnNumber.ToString()));
                        playerWins++;
                        break;
                    }
                }

                ResetGame();
            }

            Console.WriteLine("\nPlayer has won " + playerWins.ToString() + " games");
            Console.WriteLine("Enemy has won " + enemyWins.ToString() + " games");
        }

        private void ResetGame()
        {
            turnNumber = STARTING_TURN_NUMBER;

            playerDeck = new Deck();
            playerHand.Clear();

            playerHeroAttack = STARTING_HERO_ATTACK;
            playerHeroHealth = STARTING_HERO_HEALTH;

            enemyDeck = new Deck();
            enemyHand.Clear();

            enemyHeroAttack = STARTING_HERO_ATTACK;
            enemyHeroHealth = STARTING_HERO_HEALTH;
        }

        private void DrawGameBoard()
        {
            // Print Turn number
            Console.WriteLine(SPLITTER_TEXT);
            Console.WriteLine(string.Format(TURN_INFO_FORMAT, turnNumber));

            // Print Enemy hero stats
            Console.WriteLine(SPLITTER_TEXT);
            Console.WriteLine("\tEnemy Hero:");
            Console.WriteLine(string.Format(HERO_INFO_FORMAT, enemyHeroHealth, enemyHeroAttack) + "\n\n\n\n");
            
            // Print Player hero stats
            Console.WriteLine("\tPlayer Hero:");
            Console.WriteLine(string.Format(HERO_INFO_FORMAT, playerHeroHealth, playerHeroAttack));
            Console.WriteLine(SPLITTER_TEXT);
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
                Console.WriteLine(string.Format(CARD_PRINT_FORMAT, i + 1, card.Name));
                i++;
            }
            Console.WriteLine(SPLITTER_TEXT);

            // Get Player input
            Console.WriteLine("What card do you want to play?");
            Console.WriteLine(SPLITTER_TEXT);

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

        public void HandlePlayerTurnSim()
        {
            if (playerDeck.Cards.Count == 0)
            {
                return;
            }
            
            // Draw Player hand
            while (playerHand.Count < 3)
            {
                Card card = playerDeck.DrawCard();
                playerHand.Add(card);
            }

            // Get card from Player hand
            switch (playerHand[0].EffectIndex)
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
            playerHand.RemoveAt(0);

            // Attack the Player hero
            if (playerHeroAttack > 0)
            {
                enemyHeroHealth -= playerHeroAttack;
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
            Console.WriteLine(string.Format(ENEMY_ACTION_FORMAT, enemyHand[0].Name));
            enemyHand.RemoveAt(0);

            // Attack the Player hero
            if (enemyHeroAttack > 0)
            {
                playerHeroHealth -= enemyHeroAttack;
            }

            // End of turn
            turnNumber++;
            Console.WriteLine();
        }

        private void HandleEnemyTurnSim()
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
            enemyHand.RemoveAt(0);

            // Attack the Player hero
            if (enemyHeroAttack > 0)
            {
                playerHeroHealth -= enemyHeroAttack;
            }

            // End of turn
            turnNumber++;
        }
    }
}
