using System;
using System.Runtime.InteropServices;

namespace N8Engine.Native
{
    // https://stackoverflow.com/questions/13656846/how-to-programmatic-disable-c-sharp-console-applications-quick-edit-modey>
    internal static class ConsoleQuickEditModeHelper
    {
        // https://docs.microsoft.com/en-us/windows/console/getconsolemode
        [DllImport("kernel32.dll")]
        private static extern bool GetConsoleMode(IntPtr consoleHandle, out uint modeOutput);
        
        // https://docs.microsoft.com/en-us/windows/console/setconsolemode
        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleMode(IntPtr consoleHandle, uint mode);
        
        private const uint ENABLE_QUICK_EDIT_MODE = 0x0040;
        private const uint ENABLE_EXTENDED_FLAGS = 0x0080;
        private static bool _isEnabled = true;
        
        public static bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                GetConsoleMode(CommonConsoleWindowInfo.StandardInputHandle, out var consoleMode);
                
                if (value)
                    consoleMode |= ENABLE_QUICK_EDIT_MODE;
                else
                    consoleMode &= ~ENABLE_QUICK_EDIT_MODE;
                consoleMode |= ENABLE_EXTENDED_FLAGS;
                
                SetConsoleMode(CommonConsoleWindowInfo.StandardInputHandle, consoleMode);
                
                _isEnabled = value;
            }
        }
    }
}