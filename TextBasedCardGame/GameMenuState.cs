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
                            OpenSettings(game);
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
            Console.CursorVisible = false;
            game.CloseGame();
        }

        /// <summary>
        /// Displays an invalid input message.
        /// </summary>
        private void PrintInvalidInput()
        {
            GameUtils.WriteAt(
                GameConstants.INVALID_INPUT_MESSAGE, 
                0, 
                GameConstants.TITLE.Length + GameConstants.GAME_MENU_OPTIONS.Length + 1
            );
            GameUtils.ClearConsoleLine(GameConstants.TITLE.Length + GameConstants.GAME_MENU_OPTIONS.Length + 2);
            Console.SetCursorPosition(0, GameConstants.TITLE.Length + GameConstants.GAME_MENU_OPTIONS.Length + 2);
        }
    }
}
