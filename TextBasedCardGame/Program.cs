namespace TextBasedCardGame
{
    static class Program
    {
        private static bool isSimEnabled = false;
        
        static void Main(string[] args)
        {
            Game game = new Game();

            if (isSimEnabled)
            {
                game.StartGameSim();
            }
            else
            {
                game.StartGame();
            }
                
        }
    }
}
