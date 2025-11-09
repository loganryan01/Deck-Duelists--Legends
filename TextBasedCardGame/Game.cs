using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedCardGame
{
    public enum EGameTurnState
    {
        Player = 0,
        Enemy = 1
    }

    public struct Player
    {
        public Deck Deck { get; set; }
        public List<Card> Hand { get; set; }
        public int HeroHealth { get; set; }
        public int HeroAttack { get; set; }
        public int Wins { get; set; }
    }

    public class Game
    {
        private bool isRunning = true;
        private bool isSim = false;
        public bool IsSim
        {
            get
            {
                return isSim;
            }
        }
        
        private int turnNumber = GameConstants.STARTING_TURN_NUMBER;
        public int TurnNumber
        {
            get {  return turnNumber; }
        }

        private int simGameNumber = 0;
        public int SimGameNumber
        {
            get { return simGameNumber; }
        }

        // Player Info
        private Player player;
        public Player Player
        {
            get { return player; }
        }

        // Enemy Info
        private Player enemy;
        public Player Enemy
        {
            get { return enemy; }
        }
        
        public Game()
        {
            player.Deck = new Deck();
            player.Hand = new List<Card>();
            player.HeroHealth = GameConstants.STARTING_HERO_HEALTH;
            player.HeroAttack = GameConstants.STARTING_HERO_ATTACK;
            player.Wins = 0;

            enemy.Deck = new Deck();
            enemy.Hand = new List<Card>();
            enemy.HeroHealth = GameConstants.STARTING_HERO_HEALTH;
            enemy.HeroAttack = GameConstants.STARTING_HERO_ATTACK;
            enemy.Wins = 0;
        }

        public void StartGame()
        {
            Update();
        }

        public void StartSim()
        {
            isSim = true;

            for (simGameNumber = 0; simGameNumber < 100; simGameNumber++) 
            {
                isRunning = true;
                Update();
                ResetGame();
            }

            // Print simulation information
            Console.WriteLine("\nPlayer has won " + player.Wins.ToString() + " games");
            Console.WriteLine("Enemy has won " + enemy.Wins.ToString() + " games");
        }

        private void ResetGame()
        {
            turnNumber = GameConstants.STARTING_TURN_NUMBER;

            player.Deck = new Deck();
            player.Hand.Clear();

            player.HeroAttack = GameConstants.STARTING_HERO_ATTACK;
            player.HeroHealth = GameConstants.STARTING_HERO_HEALTH;

            enemy.Deck = new Deck();
            enemy.Hand.Clear();

            enemy.HeroAttack = GameConstants.STARTING_HERO_ATTACK;
            enemy.HeroHealth = GameConstants.STARTING_HERO_HEALTH;
        }

        public void StopGame()
        {
            isRunning = false;
        }

        private void Update()
        {
            EGameTurnState gameTurnState = DetermineFirstTurn();
            GameTurnState gameTurnStrategy = gameTurnState == EGameTurnState.Player ? new PrePlayerTurnState() : new PreEnemyTurnState();
            GameTurnStateManager gameTurnStateManager = new GameTurnStateManager(gameTurnStrategy);

            while (isRunning)
            {
                gameTurnStateManager.DoAction(this);
            }
        }

        private static EGameTurnState DetermineFirstTurn()
        {
            Random random = new Random();
            int playerNumber = random.Next(2);
            return (EGameTurnState)playerNumber;
        }

        public void IncrementPlayerHeroAttack()
        {
            player.HeroAttack++;
        }

        public void DecrementPlayerHeroAttack()
        {
            player.HeroAttack--;
        }

        public void IncrementPlayerHeroHealth()
        {
            player.HeroHealth++;
        }

        public void DecrementPlayerHeroHealth()
        {
            player.HeroHealth--;
        }

        public void DecreasePlayerHeroHealth(int amount)
        {
            player.HeroHealth -= amount;
        }

        public void IncrementEnemyHeroAttack()
        {
            enemy.HeroAttack++;
        }

        public void DecrementEnemyHeroAttack()
        {
            enemy.HeroAttack--;
        }

        public void IncrementEnemyHeroHealth()
        {
            enemy.HeroHealth++;
        }

        public void DecrementEnemyHeroHealth()
        {
            enemy.HeroHealth--;
        }

        public void DecreaseEnemyHeroHealth(int amount)
        {
            enemy.HeroHealth -= amount;
        }

        public void IncrementTurnNumber()
        {
            turnNumber++;
        }

        public void IncrementPlayerWinNumber()
        {
            player.Wins++;
        }

        public void IncrementEnemyWinNumber()
        {
            enemy.Wins++;
        }
    }
}
