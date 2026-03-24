namespace TextBasedCardGame
{
    /// <summary>
    /// Responsible for rendering the game log.
    /// </summary>
    public static class GameLogRenderer
    {
        /// <summary>
        /// Draws the log panel on the right side of the screen.
        /// </summary>
        public static void DrawLog()
        {
            // Draw vertical borders
            for (int i = 0; i < GameConstants.LOG_WIDTH; i++)
            {
                GameUtils.WriteAt(
                    GameConstants.LOG_VERTICAL_BORDER,
                    GameConstants.SPLITTER_TEXT.Length,
                    i
                );

                GameUtils.WriteAt(
                    GameConstants.LOG_VERTICAL_BORDER,
                    (GameConstants.SPLITTER_TEXT.Length * 2) + GameConstants.LOG_VERTICAL_BORDER.Length,
                    i
                );
            }

            // Log header
            GameUtils.WriteAt(
                GameConstants.SPLITTER_TEXT,
                GameConstants.SPLITTER_TEXT.Length + GameConstants.LOG_VERTICAL_BORDER.Length,
                0
            );

            GameUtils.WriteAt(
                GameConstants.LOG_TITLE,
                GameConstants.SPLITTER_TEXT.Length + GameConstants.LOG_VERTICAL_BORDER.Length + ((GameConstants.SPLITTER_TEXT.Length / 2) - GameConstants.LOG_TITLE.Length / 2),
                GameConstants.LOG_BOX_ONE_Y_POSITION
            );

            GameUtils.WriteAt(
                GameConstants.SPLITTER_TEXT,
                GameConstants.SPLITTER_TEXT.Length + GameConstants.LOG_VERTICAL_BORDER.Length,
                GameConstants.LOG_BOX_ONE_Y_POSITION + GameConstants.LOG_BOX_ONE_WIDTH
            );

            GameUtils.WriteAt(
                GameConstants.SPLITTER_TEXT,
                GameConstants.SPLITTER_TEXT.Length + GameConstants.LOG_VERTICAL_BORDER.Length,
                GameConstants.LOG_BOX_TWO_Y_POSITION + GameConstants.MAX_LOG_SIZE + 1
            );
        }

        /// <summary>
        /// Updates the log display with current log entries.
        /// </summary>
        public static void UpdateLog(List<string> log)
        {
            for (int i = 0; i < log.Count; i++)
            {
                ClearLogLine(i + GameConstants.LOG_BOX_TWO_Y_POSITION);
                GameUtils.WriteAt(log[i], GameConstants.SPLITTER_TEXT.Length + GameConstants.LOG_VERTICAL_BORDER.Length, i + GameConstants.LOG_BOX_TWO_Y_POSITION);
            }
        }

        /// <summary>
        /// Clears the log section.
        /// </summary>
        public static void ClearLog()
        {
            for (int i = GameConstants.LOG_BOX_TWO_Y_POSITION; i < GameConstants.LOG_WIDTH - 1; i++)
            {
                ClearLogLine(i);
            }
        }

        /// <summary>
        /// Clears an entire line starting from log starting position.
        /// </summary>
        private static void ClearLogLine(int y)
        {
            Console.SetCursorPosition(GameConstants.SPLITTER_TEXT.Length + GameConstants.LOG_VERTICAL_BORDER.Length, y);
            Console.Write(new string(' ', GameConstants.SPLITTER_TEXT.Length));
            Console.SetCursorPosition(GameConstants.SPLITTER_TEXT.Length + GameConstants.LOG_VERTICAL_BORDER.Length, y);
        }
    }
}
