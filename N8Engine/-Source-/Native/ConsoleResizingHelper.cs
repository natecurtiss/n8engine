using System;
using System.Runtime.InteropServices;

namespace N8Engine.Native
{
    internal static class ConsoleResizingHelper
    {
        // https://docs.microsoft.com/en-us/windows/console/getconsolewindow
        [DllImport("kernel32.dll", ExactSpelling = true)]  
        private static extern IntPtr GetConsoleWindow();
        
        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindow
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]  
        private static extern bool ShowWindow(IntPtr windowHandle, int displayType);
        
        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-deletemenu
        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getsystemmenu
        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        
        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindow
        private const int MAXIMIZE_DISPLAY_TYPE = 3;
        
        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-deletemenu
        private const int MF_BYCOMMAND = 0x00000000;
        
        // https://docs.microsoft.com/en-us/windows/win32/menurc/wm-syscommand
        private const int SC_CLOSE = 0xF060;
        private const int SC_MINIMIZE = 0xF020;
        private const int SC_MAXIMIZE = 0xF030;
        private const int SC_SIZE = 0xF000;
        private const int SC_HORIZONTAL_SCROLL = 0xF080;
        private const int SC_VERTICAL_SCROLL = 0xF070;

        private static readonly int[] _flagsToDisable = 
        {
            SC_SIZE,
            SC_HORIZONTAL_SCROLL,
            SC_VERTICAL_SCROLL,
            SC_MAXIMIZE,
            SC_MINIMIZE,
        };
        
        public static void Maximize()
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ShowWindow(GetConsoleWindow(), MAXIMIZE_DISPLAY_TYPE);
        }

        // https://social.msdn.microsoft.com/Forums/vstudio/en-US/1aa43c6c-71b9-42d4-aa00-60058a85f0eb/c-console-window-disable-resize?forum=csharpgeneral
        public static void DisableResizing()
        {
            var consoleWindow = GetConsoleWindow();
            var systemMenu = GetSystemMenu(consoleWindow, false);
            foreach (var flag in _flagsToDisable)
                DeleteMenu(systemMenu, flag, MF_BYCOMMAND);
        }
    }
}