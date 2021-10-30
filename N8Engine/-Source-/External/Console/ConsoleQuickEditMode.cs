using System;
using System.Runtime.InteropServices;

namespace N8Engine.External.Console
{
    // https://stackoverflow.com/questions/13656846/how-to-programmatic-disable-c-sharp-console-applications-quick-edit-modey
    static class ConsoleQuickEditMode
    {
        // https://docs.microsoft.com/en-us/windows/console/getconsolemode
        [DllImport("kernel32.dll")]
        static extern bool GetConsoleMode(IntPtr consoleHandle, out uint modeOutput);

        // https://docs.microsoft.com/en-us/windows/console/setconsolemode
        [DllImport("kernel32.dll")]
        static extern bool SetConsoleMode(IntPtr consoleHandle, uint mode);
        
        const uint ENABLE_QUICK_EDIT_MODE = 0x0040;
        const uint ENABLE_EXTENDED_FLAGS = 0x0080;

        public static void Enable()
        {
            GetConsoleMode(ConsoleInfo.StandardInputHandle, out var consoleMode);
            consoleMode |= ENABLE_QUICK_EDIT_MODE;
            consoleMode |= ENABLE_EXTENDED_FLAGS;
            SetConsoleMode(ConsoleInfo.StandardInputHandle, consoleMode);
        }

        public static void Disable()
        {
            GetConsoleMode(ConsoleInfo.StandardInputHandle, out var consoleMode);
            consoleMode &= ~ENABLE_QUICK_EDIT_MODE;
            consoleMode |= ENABLE_EXTENDED_FLAGS;
            SetConsoleMode(ConsoleInfo.StandardInputHandle, consoleMode);
        }
    }
}