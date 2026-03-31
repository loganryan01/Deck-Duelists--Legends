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
            foreach (var s in GameConstants.TITLE)
            {
                Console.WriteLine(s);
            }

            Console.WriteLine();

            // Print menu options
            for (int i = 0; i < GameConstants.GAME_MENU_OPTIONS.Length; i++)
            {
                Console.WriteLine(string.Format(GameConstants.CARD_PRINT_FORMAT, (i + 1).ToString(), GameConstants.GAME_MENU_OPTIONS[i]));
            }

            bool successfulInput = false;

            // Continue asking for input until a valid option is chosen
            while (!successfulInput)
            {
                try
                {
                    Console.CursorVisible = true;

                    int playerInput = Convert.ToInt32(Console.ReadLine());

                    switch (playerInput)
                    {
                        case 1:
                            StartGame(game);
                            successfulInput = true;
                            break;

                        case 2:
                            OpenSettings();
                            successfulInput = true;
                            break;

                        case 3:
                            ExitGame(game);
                            successfulInput = true;
                            break;

                        default:
                            PrintInvalidInput();
                            break;
                    }
                }
                catch
                {
                    PrintInvalidInput();
                }
            }
        }

        /// <summary>
        /// Starts the gameplay state.
        /// </summary>
        private void StartGame(Game game)
        {
            Console.CursorVisible = false;
            Console.Clear();

            gameStateManager.TransitionTo(new GamePlayState(), game);
        }

        /// <summary>
        /// Opens the settings menu.
        /// </summary>
        private void OpenSettings(Game game)
        {
            Console.CursorVisible = false;
            Console.Clear();

            gameStateManager.TransitionTo(new GameSettingsState(), game);
        }

        /// <summary>
        /// Exits the game.
        /// </summary>
        private void ExitGame(Game game)
        {
            Console.CursorVisible = false;
            game.CloseGame();
        }

        /// <summary>
        /// Displays an invalid input message.
        /// </summary>
        private void PrintInvalidInput()
        {
            Console.WriteLine(GameConstants.INVALID_INPUT_MESSAGE);
        }
    }
}
