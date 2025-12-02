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
        private readonly Player player;
        public Player Player
        {
            get { return player; }
        }

        // Enemy Info
        private readonly Player enemy;
        public Player Enemy
        {
            get { return enemy; }
        }
        
        public Game()
        {
            player = new Player();
            enemy = new Player();
        }

        public void StartGame()
        {
            GameStateManager gameStateManager = new GameStateManager(new GameMenuState());

            while (isRunning)
            {
                gameStateManager.DoAction(this);
            }
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

            player.Reset();
            enemy.Reset();
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

        public void IncrementTurnNumber()
        {
            turnNumber++;
        }
    }
}
