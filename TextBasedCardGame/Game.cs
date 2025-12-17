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
        public bool IsRunning
        {
            get { return isRunning; }
        }

        private bool isPlaying = true;
        public bool IsPlaying
        {
            get { return isPlaying; }
        }

        private bool isLogEnabled = true;
        public bool IsLogEnabled
        { 
            get { return isLogEnabled; } 
        }
        
        private int turnNumber = GameConstants.STARTING_TURN_NUMBER;
        public int TurnNumber
        {
            get {  return turnNumber; }
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

        private readonly List<string> log;
        public List<string> Log
        { 
            get { return log; } 
        }
        
        public Game()
        {
            player = new Player();
            enemy = new Player();

            log = new List<string>();
        }

        public void StartGame()
        {
            GameStateManager gameStateManager = new GameStateManager(new GameMenuState());

            while (isRunning)
            {
                gameStateManager.DoAction(this);
            }
        }

        public void ResetGame()
        {
            turnNumber = GameConstants.STARTING_TURN_NUMBER;
            isPlaying = true;

            player.Reset();
            enemy.Reset();

            log.Clear();
        }

        public void CloseGame()
        {
            isRunning = false;
        }

        public void StopGame()
        {
            isPlaying = false;
        }

        public void IncrementTurnNumber()
        {
            turnNumber++;
        }

        public void AddToLog(string playerName, string cardName)
        {
            // Remove the first message in the log if it exceeds the maximum size
            if (log.Count > 13)
            {
                log.RemoveAt(0);
            }
            
            string message = string.Format(GameConstants.LOG_FORMAT, playerName, cardName);
            log.Add(message);
        }

        public void EnableLog(bool enable)
        {
            isLogEnabled = enable;
        }
    }
}
