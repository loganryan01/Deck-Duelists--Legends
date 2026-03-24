using System;

namespace TextBasedCardGame
{
    /// <summary>
    /// Utility class responsible for drawing and updating the console UI.
    /// </summary>
    public static class GameUtils
    {
        //-----------------------------------------------
        // TEXT RENDERING
        //-----------------------------------------------

        /// <summary>
        /// Writes text to the console at a specific position.
        /// </summary>
        public static void WriteAt(string s, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(s);
        }

        //-----------------------------------------------
        // LINE CLEARING
        //-----------------------------------------------

        /// <summary>
        /// Clears an entire line starting from column 0.
        /// </summary>
        public static void ClearConsoleLine(int y)
        {
            Console.SetCursorPosition(0, y);
            Console.Write(new string(' ', GameConstants.SPLITTER_TEXT.Length));
            Console.SetCursorPosition(0, y);
        }
    }
}
