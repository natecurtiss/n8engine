using System;
using System.Runtime.InteropServices;

namespace N8Engine.Native
{
    internal static class ConsoleResizingHelper
    {
        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindow
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]  
        private static extern bool ShowWindow(IntPtr windowHandle, int displayType);

        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowlonga
        [DllImport("user32.dll")]
        private static extern uint GetWindowLong(IntPtr hWnd, int nIndex);
        
        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowlonga
        [DllImport("user32.dll")]
        private static extern uint SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindow
        private const int MAXIMIZE_DISPLAY_TYPE = 3;

        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowlonga
        private const int NEW_WINDOW_STYLE = -16;
        // http://pinvoke.net/default.aspx/Constants/Window%20styles.html
        private const uint WS_MAXIMIZE_BOX = 0x00010000;
        private const uint WS_MINIMIZE_BOX = 0x00020000;
        private const uint WS_SIZE_BOX = 0x00040000;
        private const uint WS_BORDER = 0x00800000;

        public static void Maximize()
        {
            ToggleFullscreen();
            RemoveTitlebar();
            RemoveScrollbar();
        }

        private static void ToggleFullscreen()
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ShowWindow(CommonConsoleWindowInfo.Handle, MAXIMIZE_DISPLAY_TYPE);
        }
        
        // https://stackoverflow.com/questions/41172595/how-to-change-console-window-style-at-runtime
        private static void RemoveTitlebar()
        {
            var currentWindowStyle = GetWindowLong(CommonConsoleWindowInfo.Handle, NEW_WINDOW_STYLE);
            SetWindowLong(CommonConsoleWindowInfo.Handle, NEW_WINDOW_STYLE, 
                currentWindowStyle & 
                ~WS_MAXIMIZE_BOX & 
                ~WS_MINIMIZE_BOX & 
                ~WS_SIZE_BOX & 
                ~WS_BORDER
            );
        }

        // https://stackoverflow.com/questions/50163122/how-to-remove-scroll-bar-from-fullscreen-console-in-c
        private static void RemoveScrollbar() => Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
    }
}