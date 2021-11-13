using System;
using System.Runtime.InteropServices;
using N8Engine.Mathematics;
using static N8Engine.External.ExtStructs;

namespace N8Engine.External
{
    static class ExtWindow
    {
        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowpos
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowlonga
        [DllImport("user32.dll")]
        static extern uint GetWindowLong(IntPtr hWnd, int nIndex);
        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowlonga
        [DllImport("user32.dll")]
        static extern uint SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
        
        // https://www.pinvoke.net/default.aspx/user32.showwindow
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        
        // https://www.pinvoke.net/default.aspx/user32.setwindowtext
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern bool SetWindowText(IntPtr hWnd, string lpString);
        
        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setprocessdpiaware
        [DllImport("user32.dll", SetLastError=true)]
        static extern bool SetProcessDPIAware();
        
        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowrect
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getsystemmetrics?redirectedfrom=MSDN
        [DllImport("user32.dll")]
        static extern int GetSystemMetrics(int nIndex);

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
        
        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getsystemmetrics?redirectedfrom=MSDN
        const int SM_CX_SCREEN = 0;
        const int SM_CY_SCREEN = 1;

        static bool _isDpiAware;

        public static void SetTitle(IntPtr handle, string title) => SetWindowText(handle, title);

        public static void Resize(IntPtr handle, IntVector size)
        {
            if (size == GetMonitorSize())
                Maximize(handle);
            var center = GetCenter(handle);
            SetWindowPos(handle, new IntPtr(0), center.X, center.Y, size.X, size.Y, SWP_SHOW_WINDOW);
        }

        public static void Show(IntPtr handle) => ShowWindow(handle, SW_SHOW);
        public static void Hide(IntPtr handle) => ShowWindow(handle, SW_HIDE);

        // https://stackoverflow.com/questions/41172595/how-to-change-console-window-style-at-runtime
        public static void DisableResizing(IntPtr handle)
        {
            var currentWindowStyle = GetWindowLong(handle, NEW_WINDOW_STYLE);
            SetWindowLong(handle, NEW_WINDOW_STYLE, 
                currentWindowStyle & 
                ~WS_MAXIMIZE_BOX & 
                ~WS_MINIMIZE_BOX & 
                ~WS_SIZE_BOX |
                WS_CAPTION |
                WS_POPUP
            );
        }
        
        public static void Maximize(IntPtr handle) => ShowWindow(handle, SW_MAXIMIZE);
        
        public static IntVector GetSize(IntPtr handle)
        {
            if (!_isDpiAware)
            {
                SetProcessDPIAware();
                _isDpiAware = true;
            }
            GetWindowRect(handle, out var rect);
            var width = rect.Right - rect.Left;
            var height = rect.Bottom - rect.Top;
            return new IntVector(width, height);
        }

        public static IntVector GetCenter(IntPtr handle)
        {
            var difference = GetMonitorSize() / 2 - GetSize(handle) / 2;
            return difference;
        }

        public static IntVector GetMonitorSize()
        {
            if (!_isDpiAware)
            {
                SetProcessDPIAware();
                _isDpiAware = true;
            }
            return new(GetSystemMetrics(SM_CX_SCREEN), GetSystemMetrics(SM_CY_SCREEN));
        }
    }
}