using System;
using System.Runtime.InteropServices;

namespace N8Engine.External
{
    internal static class ConsoleModeHelper
    {
        private const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;
        private const uint DISABLE_NEWLINE_AUTO_RETURN = 0x0008;

        [DllImport("kernel32.dll")]
        private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        public static void EnableAnsiEscapeSequences()
        {
            GetConsoleMode(CommonConsoleWindowInfo.StandardOutputHandle, out var consoleMode);
            consoleMode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING | DISABLE_NEWLINE_AUTO_RETURN;
            SetConsoleMode(CommonConsoleWindowInfo.StandardOutputHandle, consoleMode);
        }
    }
}