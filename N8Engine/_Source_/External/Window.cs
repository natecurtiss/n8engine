using System;
using System.Runtime.InteropServices;
using N8Engine.Mathematics;
using static N8Engine.External.Structs;

namespace N8Engine.External
{
    abstract class Window
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

        bool _isDpiAware;

        protected abstract IntPtr Handle { get; }

        protected void SetTitle(string title) => SetWindowText(Handle, title);

        protected void Resize(IntVector size)
        {
            if (size == GetMonitorSize())
                Maximize();
            var center = GetCenter();
            SetWindowPos(Handle, new IntPtr(0), center.X, center.Y, size.X, size.Y, SWP_SHOW_WINDOW);
        }

        protected void Show() => ShowWindow(Handle, SW_SHOW);
        protected void Hide() => ShowWindow(Handle, SW_HIDE);

        // https://stackoverflow.com/questions/41172595/how-to-change-console-window-style-at-runtime
        protected void DisableResizing()
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
        
        void Maximize() => ShowWindow(Handle, SW_MAXIMIZE);
        
        IntVector GetSize()
        {
            if (!_isDpiAware)
            {
                SetProcessDPIAware();
                _isDpiAware = true;
            }
            GetWindowRect(Handle, out var rect);
            var width = rect.Right - rect.Left;
            var height = rect.Bottom - rect.Top;
            return new IntVector(width, height);
        }

        IntVector GetCenter()
        {
            var difference = GetMonitorSize() / 2 - GetSize() / 2;
            return difference;
        }

        IntVector GetMonitorSize()
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