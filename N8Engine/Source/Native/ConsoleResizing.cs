using System;
using System.Runtime.InteropServices;

namespace N8Engine.Native
{
    internal static class ConsoleResizing
    {
        /// <summary>
        /// Returns the Console window.
        /// </summary>
        /// <returns> The Console window. </returns>
        [DllImport("kernel32.dll", ExactSpelling = true)]  
        private static extern IntPtr GetConsoleWindow();
        
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