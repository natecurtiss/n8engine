using System;
using System.Runtime.InteropServices;
using N8Engine.Mathematics;
using static N8Engine.External.Console.ConsoleInfo;
using SysConsole = System.Console;

namespace N8Engine.External.User
{
    static class UserWindow
    {
        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowpos
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowlonga
        [DllImport("user32.dll")]
        static extern uint GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        static extern uint SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
        
        // https://www.pinvoke.net/default.aspx/user32.showwindow
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        //https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowpos
        const uint SWP_SHOW_WINDOW = 0x0040;
        
        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowlonga
        const int NEW_WINDOW_STYLE = -16;
        
        // https://docs.microsoft.com/en-us/windows/win32/winmsg/window-styles
        // http://pinvoke.net/default.aspx/Constants/Window%20styles.html
        const uint WS_MAXIMIZE_BOX = 0x00010000;
        const uint WS_MINIMIZE_BOX = 0x00020000;
        const uint WS_SIZE_BOX = 0x00040000;
        const uint WS_CAPTION = 0x00C00000;
        const uint WS_POPUP = 0x80000000;
        
        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindow
        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        public static void RemoveTitlebarAndScrollbar()
        {
            RemoveTitlebar(); 
            RemoveScrollbar();
        }

        public static void Resize(IntVector size)
        {
            var center = UserMetrics.GetCenterOfWindow(size);
            SetWindowPos(Handle, IntPtr.Zero, center.X, center.Y, size.X, size.Y, SWP_SHOW_WINDOW);
        }

        // https://stackoverflow.com/questions/41172595/how-to-change-console-window-style-at-runtime
        static void RemoveTitlebar()
        {
            var currentWindowStyle = GetWindowLong(Handle, NEW_WINDOW_STYLE);
            SetWindowLong(Handle, NEW_WINDOW_STYLE, 
                currentWindowStyle & 
                ~WS_MAXIMIZE_BOX & 
                ~WS_MINIMIZE_BOX & 
                ~WS_SIZE_BOX |
                WS_CAPTION |
                WS_POPUP
            );
        }

        // TODO: make this work on all platforms.
        // https://stackoverflow.com/questions/50163122/how-to-remove-scroll-bar-from-fullscreen-console-in-c
        static void RemoveScrollbar() => SysConsole.SetBufferSize(SysConsole.WindowWidth, SysConsole.WindowHeight);
        
        public static void Show(IntPtr windowHandle) => ShowWindow(windowHandle, SW_SHOW);
        public static void Hide(IntPtr windowHandle) => ShowWindow(windowHandle, SW_HIDE);
    }
}