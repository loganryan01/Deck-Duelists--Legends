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

        public void ResetGame()
        {
            turnNumber = GameConstants.STARTING_TURN_NUMBER;
            isPlaying = true;

            player.Reset();
            enemy.Reset();
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
    }
}
