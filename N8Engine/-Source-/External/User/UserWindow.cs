using System;
using System.Runtime.InteropServices;
using N8Engine.Mathematics;

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
        
        // https://www.pinvoke.net/default.aspx/user32.setwindowtext
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern bool SetWindowText(IntPtr hWnd, string lpString);

        //https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowpos
        const uint SWP_SHOW_WINDOW = 0x0040;
        const int SW_MAXIMIZE = 3;
        
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

        public static void Resize(IntPtr windowHandle, IntVector size)
        {
            if (size == UserMetrics.MonitorSize)
                Maximize(windowHandle);
            var center = UserMetrics.GetCenterOfWindow(size);
            SetWindowPos(windowHandle, new IntPtr(0), center.X, center.Y, size.X, size.Y, SWP_SHOW_WINDOW);
        }

        public static void Maximize(IntPtr windowHandle) => ShowWindow(windowHandle, SW_MAXIMIZE);
        public static void Show(IntPtr windowHandle) => ShowWindow(windowHandle, SW_SHOW);
        public static void Hide(IntPtr windowHandle) => ShowWindow(windowHandle, SW_HIDE);

        public static void SetTitle(IntPtr windowHandle, string title) => SetWindowText(windowHandle, title);

        // https://stackoverflow.com/questions/41172595/how-to-change-console-window-style-at-runtime
        public static void RemoveTitlebar(IntPtr windowHandle)
        {
            var currentWindowStyle = GetWindowLong(windowHandle, NEW_WINDOW_STYLE);
            SetWindowLong(windowHandle, NEW_WINDOW_STYLE, 
                currentWindowStyle & 
                ~WS_MAXIMIZE_BOX & 
                ~WS_MINIMIZE_BOX & 
                ~WS_SIZE_BOX |
                WS_CAPTION |
                WS_POPUP
            );
        }
    }
}