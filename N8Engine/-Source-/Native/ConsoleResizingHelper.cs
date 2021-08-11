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

        private const int MAXIMIZE_DISPLAY_TYPE = 3;
        
        public static void Maximize()
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ShowWindow(GetConsoleWindow(), MAXIMIZE_DISPLAY_TYPE);
        }
    }
}