using System;
using System.Runtime.InteropServices;

namespace N8Engine.External
{
    static class Terminal
    {
        // https://docs.microsoft.com/en-us/windows/console/getstdhandle
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);
        
        // https://docs.microsoft.com/en-us/windows/console/getconsolewindow
        [DllImport("kernel32.dll", ExactSpelling = true)]
        static extern IntPtr GetConsoleWindow();
        
        // https://docs.microsoft.com/en-us/windows/console/getconsolemode
        [DllImport("kernel32.dll")]
        static extern bool GetConsoleMode(IntPtr consoleHandle, out uint modeOutput);
        // https://docs.microsoft.com/en-us/windows/console/setconsolemode
        [DllImport("kernel32.dll")]
        static extern bool SetConsoleMode(IntPtr consoleHandle, uint mode);

        const int STANDARD_INPUT_HANDLE_NUMBER = -10;
        const int STANDARD_OUTPUT_HANDLE_NUMBER = -11;
        const int STANDARD_ERROR_HANDLE_NUMBER = -12;

        public static readonly IntPtr StandardInputHandle = GetStdHandle(STANDARD_INPUT_HANDLE_NUMBER);
        public static readonly IntPtr StandardOutputHandle = GetStdHandle(STANDARD_OUTPUT_HANDLE_NUMBER);
        public static readonly IntPtr StandardErrorHandle = GetStdHandle(STANDARD_ERROR_HANDLE_NUMBER);
        public static readonly IntPtr Handle = GetConsoleWindow();

        const uint ENABLE_QUICK_EDIT_MODE = 0x0040;
        const uint ENABLE_EXTENDED_FLAGS = 0x0080;

        // https://stackoverflow.com/questions/13656846/how-to-programmatic-disable-c-sharp-console-applications-quick-edit-modey
        public static void EnableQuickEditMode()
        {
            GetConsoleMode(StandardInputHandle, out var consoleMode);
            consoleMode |= ENABLE_QUICK_EDIT_MODE;
            consoleMode |= ENABLE_EXTENDED_FLAGS;
            SetConsoleMode(StandardInputHandle, consoleMode);
        }

        public static void DisableQuickEditMode()
        {
            GetConsoleMode(StandardInputHandle, out var consoleMode);
            consoleMode &= ~ENABLE_QUICK_EDIT_MODE;
            consoleMode |= ENABLE_EXTENDED_FLAGS;
            SetConsoleMode(StandardInputHandle, consoleMode);
        }

        public static void RemoveScrollbar() => Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
    }
}