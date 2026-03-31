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
            while (!successfulInput)
            {
                try
                {
                    Console.CursorVisible = true;
                    int playerInput = Convert.ToInt32(Console.ReadLine());

                    if (playerInput == 1)
                    {
                        Console.CursorVisible = false;
                        successfulInput = true;

                        GameUtils.ClearConsoleLine(GameConstants.TITLE.Length + 1);
                        GameUtils.ClearConsoleLine(GameConstants.TITLE.Length + 5);

                        game.EnableLog(!game.IsLogEnabled);
                    }
                    else if (playerInput == 2)
                    {
                        Console.CursorVisible = false;
                        successfulInput = true;

                        GameUtils.ClearConsoleLine(GameConstants.TITLE.Length + 5);

                        game.UpdateChosenFormat();
                    }
                    else if (playerInput == 3)
                    {
                        Console.CursorVisible = false;
                        successfulInput = true;

                        for (int i = 1; i < 6; i++)
                        {
                            GameUtils.ClearConsoleLine(GameConstants.TITLE.Length + i + 1);
                        }
                        gameStateManager.TransitionTo(new GameSettingsRoundState(), game);
                    }
                    else if (playerInput == 4)
                    {
                        Console.CursorVisible = false;
                        successfulInput = true;

                        for (int i = 1; i < 7; i++)
                        {
                            GameUtils.ClearConsoleLine(GameConstants.TITLE.Length + i);
                        }
                        gameStateManager.TransitionTo(new GameMenuState(), game);
                    }
                    else
                    {
                        GameUtils.WriteAt(GameConstants.INVALID_INPUT_MESSAGE, 0, GameConstants.TITLE.Length + 5);
                        GameUtils.ClearConsoleLine(GameConstants.TITLE.Length + 6);
                        Console.SetCursorPosition(0, GameConstants.TITLE.Length + 6);
                    }
                }
                catch (Exception)
                {
                    GameUtils.WriteAt(GameConstants.INVALID_INPUT_MESSAGE, 0, GameConstants.TITLE.Length + 5);
                    GameUtils.ClearConsoleLine(GameConstants.TITLE.Length + 6);
                    Console.SetCursorPosition(0, GameConstants.TITLE.Length + 6);
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
