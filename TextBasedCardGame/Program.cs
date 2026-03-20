using System.Runtime.InteropServices;

namespace TextBasedCardGame
{
    /// <summary>
    /// Entry point for the application.
    /// Creates and configures the console window before starting the game.
    /// </summary>
    static class Program
    {
        // Window style constants used to modify the console window
        const int GWL_STYLE = -16;
        const int WS_MAXIMIZEBOX = 0x00010000;
        const int WS_THICKFRAME = 0x00040000;

        // Window positioning flags
        const uint SWP_NOMOVE = 0x0002;
        const uint SWP_NOSIZE = 0x0001;
        const uint SWP_NOZORDER = 0x0004;
        const uint SWP_FRAMECHANGED = 0x0020;

        // Allocates a new console for the application
        [DllImport("Kernel32.dll")]
        static extern bool AllocConsole();

        // Retrieves the window handle for the console
        [DllImport("Kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        // Gets the current window style
        [DllImport("User32.dll")]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        // Sets a new window style
        [DllImport("User32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        // Updates the window position or style
        [DllImport("User32.dll")]
        static extern bool SetWindowPos(
            IntPtr hWnd,
            IntPtr hWndInsertAfter,
            int X, 
            int Y, 
            int cx, 
            int cy,
            uint uFlags);

        static void Main(string[] args)
        {
            // Setup the console window
            SetupConsoleWindow();

            // Hide the blinking cursor for a cleaner UI
            Console.CursorVisible = false;

            // Create and start the game
            Game game = new Game();
            game.StartGame();
        }

        private static void SetupConsoleWindow()
        {
            // Create a console window (since this project is a Windows Application)
            AllocConsole();

            // Get the console window handle
            IntPtr consoleWindow = GetConsoleWindow();

            // Retrieve the current window style
            int windowStyle = GetWindowLong(consoleWindow, GWL_STYLE);

            // Remove the maximize button
            windowStyle &= ~WS_MAXIMIZEBOX;

            // Remove the resize border
            windowStyle &= ~WS_THICKFRAME;

            // Apply the updated window style
            SetWindowLong(consoleWindow, GWL_STYLE, windowStyle);

            // Force the window to refresh with the new style
            SetWindowPos(
                consoleWindow,
                IntPtr.Zero,
                0,
                0,
                0,
                0,
                SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED
            );
        }
    }
}
