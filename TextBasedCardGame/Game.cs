using System;
using System.Collections.Generic;

namespace TextBasedCardGame
{
    /// <summary>
    /// Determines whose turn it currently is
    /// </summary>
    public enum EGameTurnState
    {
        Player = 0,
        Enemy = 1
    }

    /// <summary>
    /// Main game controller.
    /// Stores game state and manages players, rounds, turns, and logs.
    /// </summary>
    public class Game
    {
        //==================================================
        // Core Game State
        //==================================================

        private bool isRunning = true;
        public bool IsRunning => isRunning;

        private bool isPlaying = true;
        public bool IsPlaying => isPlaying;

        private bool isLogEnabled = true;
        public bool IsLogEnabled => isLogEnabled;

        //==================================================
        // Turn & Round Information
        //==================================================

        private int turnNumber = GameConstants.STARTING_TURN_NUMBER;
        public int TurnNumber => turnNumber;

        private int numberOfRounds = GameConstants.DEFAULT_NUMBER_OF_ROUNDS;
        public int NumberOfRounds => numberOfRounds;

        private int currentRound = 1;
        public int CurrentRound => currentRound;

        private string currentFormat = GameConstants.GAME_FORMATS[0];
        public string CurrentFormat => currentFormat;

        //==================================================
        // Player Information
        //==================================================

        private readonly Player player;
        public Player Player => player;

        private readonly Player enemy;
        public Player Enemy => enemy;

        //==================================================
        // Game Log
        //==================================================

        private readonly List<string> log;
        public List<string> Log => log;

        //==================================================
        // Constructor
        //==================================================

        public Game()
        {
            player = new Player();
            enemy = new Player();

            log = new List<string>();
        }

        //==================================================
        // Game Loop
        //==================================================

        /// <summary>
        /// Starts the main game loop.
        /// The GameStateManager controls which state is active.
        /// </summary>
        public void StartGame()
        {
            GameStateManager gameStateManager = new GameStateManager(new GameMenuState());

            while (isRunning)
            {
                gameStateManager.DoAction(this);
            }
        }

        //==================================================
        // Game Control
        //==================================================

        /// <summary>
        /// Resets the game state for a new match.
        /// </summary>
        public void ResetGame()
        {
            turnNumber = GameConstants.STARTING_TURN_NUMBER;
            isPlaying = true;

            player.Reset();
            enemy.Reset();

            log.Clear();
        }

        /// <summary>
        /// Completely closes the game.
        /// </summary>
        public void CloseGame()
        {
            isRunning = false;
        }

        /// <summary>
        /// Stops the current match but keeps the program running.
        /// </summary>
        public void StopGame()
        {
            isPlaying = false;
        }

        //==================================================
        // Turn Management
        //==================================================

        /// <summary>
        /// Advances the turn counter.
        /// </summary>
        public void IncrementTurnNumber()
        {
            turnNumber++;
        }

        //==================================================
        // Game Log System
        //==================================================

        /// <summary>
        /// Adds a message to the game log.
        /// Keeps the log size limited.
        /// </summary>
        public void AddToLog(string playerName, string cardName)
        {
            // Remove the oldest message if the log exceeds the maximum size
            if (log.Count > GameConstants.MAX_LOG_SIZE)
            {
                log.RemoveAt(0);
            }
            
            string message = string.Format(
                GameConstants.LOG_FORMAT, 
                turnNumber.ToString(), 
                playerName, 
                cardName
            );

            log.Add(message);
        }

        /// <summary>
        /// Enables or disables the game log.
        /// </summary>
        /// <param name="enable"></param>
        public void EnableLog(bool enable)
        {
            isLogEnabled = enable;
        }

        //==================================================
        // Round Management
        //==================================================

        /// <summary>
        /// Sets the number of rounds in the match.
        /// </summary>
        /// <param name="numberOfRounds"></param>
        public void SetNumberOfRounds(int numberOfRounds)
        {
            this.numberOfRounds = numberOfRounds;
        }

        /// <summary>
        /// Moves the game to the next round.
        /// </summary>
        public void IncrementCurrentRound()
        {
            currentRound++;
        }

        //==================================================
        // Game Format Settings
        //==================================================

        /// <summary>
        /// Displays the currently selected game format.
        /// </summary>
        /// <param name="settingsIndex"></param>
        public void PrintChosenFormat(int settingsIndex)
        {
            Console.ResetColor();
            Console.Write(string.Format(GameConstants.INDEX_PRINT_FORMAT, settingsIndex));

            for (int i = 0; i < GameConstants.GAME_FORMATS.Length; i++)
            {
                Console.ResetColor();

                string format = GameConstants.GAME_FORMATS[i];

                if (i != 0)
                    Console.Write(GameConstants.FORMAT_DIVIDER);

                // Highlight the currently selected format
                if (format == currentFormat)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }

                Console.Write(format);
            }

            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// Cycles to the next available game format.
        /// </summary>
        public void UpdateChosenFormat()
        {
            int currentIndex = Array.IndexOf(GameConstants.GAME_FORMATS, currentFormat);

            if (currentIndex != -1)
            {
                currentIndex++;
                currentIndex %= GameConstants.GAME_FORMATS.Length;

                currentFormat = GameConstants.GAME_FORMATS[currentIndex];
            }
        }
    }
}
