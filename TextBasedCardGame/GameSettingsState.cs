namespace TextBasedCardGame
{
    public class GameSettingsState : GameState
    {
        public override void DoAction(Game game)
        {
            foreach (var s in GameConstants.TITLE)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine();
            Console.WriteLine(string.Format(GameConstants.CARD_PRINT_FORMAT, "1", string.Format(GameConstants.LOG_SETTING_FORMAT, game.IsLogEnabled)));
            game.PrintChosenFormat(2);
            Console.WriteLine(string.Format(GameConstants.CARD_PRINT_FORMAT, "3", string.Format(game.CurrentFormat + " = {0}", game.NumberOfRounds)));
            Console.WriteLine(string.Format(GameConstants.CARD_PRINT_FORMAT, "4", GameConstants.BACK_OPTION_TEXT));

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

                        Console.Clear();
                        game.EnableLog(!game.IsLogEnabled);
                    }
                    else if (playerInput == 2)
                    {
                        Console.CursorVisible = false;
                        successfulInput = true;

                        Console.Clear();
                        game.UpdateChosenFormat();
                    }
                    else if (playerInput == 3)
                    {
                        Console.CursorVisible = false;
                        successfulInput = true;

                        Console.Clear();
                        gameStateManager.TransitionTo(new GameSettingsRoundState());
                    }
                    else if (playerInput == 4)
                    {
                        Console.CursorVisible = false;
                        successfulInput = true;

                        Console.Clear();
                        gameStateManager.TransitionTo(new GameMenuState());
                    }
                    else
                    {
                        Console.WriteLine(GameConstants.INVALID_INPUT_MESSAGE);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine(GameConstants.INVALID_INPUT_MESSAGE);
                }
            }
        }
    }
}
