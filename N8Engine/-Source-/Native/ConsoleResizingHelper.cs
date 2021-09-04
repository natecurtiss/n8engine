using System;
using System.Runtime.InteropServices;
using N8Engine.Mathematics;

namespace N8Engine.Native
{
    internal static class ConsoleResizingHelper
    {
        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindow
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int displayType);
        
        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowpos
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowlonga
        [DllImport("user32.dll")]
        private static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowlonga
        [DllImport("user32.dll")]
        private static extern uint SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
        
        // https://www.pinvoke.net/default.aspx/user32.setwindowpos
        private static readonly IntPtr _top = new(0);

        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindow
        private const int MAXIMIZE_DISPLAY_TYPE = 3;

        //https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowpos
        private const uint SWP_SHOW_WINDOW = 0x0040;

        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowlonga
        private const int NEW_WINDOW_STYLE = -16;
        // https://docs.microsoft.com/en-us/windows/win32/winmsg/window-styles
        // http://pinvoke.net/default.aspx/Constants/Window%20styles.html
        private const uint WS_MAXIMIZE_BOX = 0x00010000;
        private const uint WS_MINIMIZE_BOX = 0x00020000;
        private const uint WS_SIZE_BOX = 0x00040000;
        private const uint WS_CAPTION = 0x00C00000;
        private const uint WS_DLG_FRAME = 0x00400000;
        private const uint WS_POPUP = 0x80000000;

        public static void RemoveTitlebarAndScrollbar()
        {
            RemoveTitlebar(); 
            RemoveScrollbar();
        }

        public static void Maximize() => ToggleFullscreen();

        private static void ToggleFullscreen()
        {
            var windowSize = new IntegerVector(1280, 720);
            var center = CommonConsoleWindowInfo.CenterOfScreenFromWindowSize(windowSize);
            SetWindowPos(CommonConsoleWindowInfo.Handle, _top, center.X, center.Y, windowSize.X, windowSize.Y, SWP_SHOW_WINDOW);
            // Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            // ShowWindow(CommonConsoleWindowInfo.Handle, MAXIMIZE_DISPLAY_TYPE);
        }
        
        // https://stackoverflow.com/questions/41172595/how-to-change-console-window-style-at-runtime
        private static void RemoveTitlebar()
        {
            var currentWindowStyle = GetWindowLong(CommonConsoleWindowInfo.Handle, NEW_WINDOW_STYLE);
            SetWindowLong(CommonConsoleWindowInfo.Handle, NEW_WINDOW_STYLE, 
                currentWindowStyle & 
                ~WS_MAXIMIZE_BOX & 
                ~WS_MINIMIZE_BOX & 
                ~WS_SIZE_BOX |
                WS_CAPTION |
                WS_POPUP
            );
        }

        // https://stackoverflow.com/questions/50163122/how-to-remove-scroll-bar-from-fullscreen-console-in-c
        private static void RemoveScrollbar() => Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
    }
}