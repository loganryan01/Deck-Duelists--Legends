using System;

namespace TextBasedCardGame
{
    /// <summary>
    /// Main menu state of the game.
    /// Allows the player to start the game, open settings, or exit.
    /// </summary>
    public class GameMenuState : GameState
    {
        public override void DoAction(Game game)
        {
            // Print the game title
            for (int i = 0; i < GameConstants.TITLE.Length; i++)
            {
                GameUtils.WriteAt(GameConstants.TITLE[i], 0, i);
            }

            // Print menu options
            for (int i = 0; i < GameConstants.GAME_MENU_OPTIONS.Length; i++)
            {
                GameUtils.WriteAt(
                    string.Format(GameConstants.CARD_PRINT_FORMAT, (i + 1).ToString(), GameConstants.GAME_MENU_OPTIONS[i]),
                    0,
                    GameConstants.TITLE.Length + i + 1
                );
            }

            Console.SetCursorPosition(0, GameConstants.TITLE.Length + GameConstants.GAME_MENU_OPTIONS.Length + 1);

            bool successfulInput = false;
            ConsoleKeyInfo playerInput;

            // Continue asking for input until a valid option is chosen
            while (!successfulInput)
            {
                // Clear input buffer
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }
                playerInput = Console.ReadKey(true);

                switch (playerInput.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        StartGame(game);
                        successfulInput = true;
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        OpenSettings(game);
                        successfulInput = true;
                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        ExitGame(game);
                        successfulInput = true;
                        break;
                }
            }
        }

        public override void Enter(Game game)
        {
            // Empty
        }

        public override void Exit(Game game)
        {
            // Empty
        }

        /// <summary>
        /// Starts the gameplay state.
        /// </summary>
        private void StartGame(Game game)
        {
            Console.Clear();

            gameStateManager.TransitionTo(new GamePlayState(), game);
        }

        /// <summary>
        /// Opens the settings menu.
        /// </summary>
        private void OpenSettings(Game game)
        {
            for (int i = 0; i < GameConstants.GAME_MENU_OPTIONS.Length + 2; i++)
            {
                GameUtils.ClearConsoleLine(GameConstants.TITLE.Length + i + 1);
            }

            gameStateManager.TransitionTo(new GameSettingsState(), game);
        }

        /// <summary>
        /// Exits the game.
        /// </summary>
        private void ExitGame(Game game)
        {
            game.CloseGame();
        }
    }
}
