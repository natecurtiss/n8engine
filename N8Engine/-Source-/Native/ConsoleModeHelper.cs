using System;
using System.Runtime.InteropServices;

namespace N8Engine.Native
{
    static class ConsoleModeHelper
    {
        const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;
        const uint DISABLE_NEWLINE_AUTO_RETURN = 0x0008;

        [DllImport("kernel32.dll")]
        static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll")]
        static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        internal static void EnableAnsiEscapeSequences()
        {
            GetConsoleMode(CommonConsoleWindowInfo.StandardOutputHandle, out var outConsoleMode);
            outConsoleMode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING | DISABLE_NEWLINE_AUTO_RETURN;
            SetConsoleMode(CommonConsoleWindowInfo.StandardOutputHandle, outConsoleMode);
        }
    }
}