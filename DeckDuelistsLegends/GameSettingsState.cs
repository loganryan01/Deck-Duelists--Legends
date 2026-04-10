namespace TextBasedCardGame
{
    public class GameSettingsState : GameState
    {
        public override void DoAction(Game game)
        {
            GameUtils.WriteAt(
                string.Format(GameConstants.CARD_PRINT_FORMAT, "1", string.Format(GameConstants.LOG_SETTING_FORMAT, game.IsLogEnabled)),
                0,
                GameConstants.TITLE.Length + 1
            );
            game.PrintChosenFormat(2);
            GameUtils.WriteAt(
                string.Format(GameConstants.CARD_PRINT_FORMAT, "3", string.Format(game.CurrentFormat + " = {0}", game.NumberOfRounds)),
                0,
                GameConstants.TITLE.Length + 3
            );
            GameUtils.WriteAt(
                string.Format(GameConstants.CARD_PRINT_FORMAT, "4", GameConstants.BACK_OPTION_TEXT),
                0,
                GameConstants.TITLE.Length + 4
            );
            Console.SetCursorPosition(0, GameConstants.TITLE.Length + 5);

            bool successfulInput = false;
            ConsoleKeyInfo playerInput;

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
                        successfulInput = true;

                        GameUtils.ClearConsoleLine(GameConstants.TITLE.Length + 1);
                        GameUtils.ClearConsoleLine(GameConstants.TITLE.Length + 5);

                        game.EnableLog(!game.IsLogEnabled);
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        successfulInput = true;

                        GameUtils.ClearConsoleLine(GameConstants.TITLE.Length + 5);

                        game.UpdateChosenFormat();
                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        successfulInput = true;

                        for (int i = 1; i < 6; i++)
                        {
                            GameUtils.ClearConsoleLine(GameConstants.TITLE.Length + i + 1);
                        }
                        gameStateManager.TransitionTo(new GameSettingsRoundState(), game);
                        break;

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        successfulInput = true;

                        for (int i = 1; i < 7; i++)
                        {
                            GameUtils.ClearConsoleLine(GameConstants.TITLE.Length + i);
                        }
                        gameStateManager.TransitionTo(new GameMenuState(), game);
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
    }
}
