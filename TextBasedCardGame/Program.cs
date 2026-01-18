using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;

namespace TextBasedCardGame
{
    static class Program
    {
        const int GWL_STYLE = -16;
        const int WS_MAXIMIZEBOX = 0x00010000;
        const int WS_THICKFRAME = 0x00040000;

        const uint SWP_NOMOVE = 0x0002;
        const uint SWP_NOSIZE = 0x0001;
        const uint SWP_NOZORDER = 0x0004;
        const uint SWP_FRAMECHANGED = 0x0020;

        [DllImport("Kernel32.dll")]
        static extern bool AllocConsole();

        [DllImport("Kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("User32.dll")]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("User32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("User32.dll")]
        static extern bool SetWindowPos(
            IntPtr hWnd,
            IntPtr hWndInsertAfter,
            int X, int Y, int cx, int cy,
            uint uFlags);

        static void Main(string[] args)
        {
            AllocConsole();
            
            IntPtr console = GetConsoleWindow();

            int style = GetWindowLong(console, GWL_STYLE);
            style &= ~WS_MAXIMIZEBOX;
            style &= ~WS_THICKFRAME;
            SetWindowLong(console, GWL_STYLE, style);

            SetWindowPos(
                console,
                IntPtr.Zero,
                0, 0, 0, 0,
                SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED
            );

            Console.CursorVisible = false;

            Game game = new Game();
            game.StartGame();
        }
    }
}
