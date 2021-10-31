using System;
using System.Runtime.InteropServices;
using N8Engine.Mathematics;
using static N8Engine.External.CommonStructures;

namespace N8Engine.External.User
{
    static class UserMetrics
    {
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
        
        const int SM_CX_SCREEN = 0;
        const int SM_CY_SCREEN = 1;

        static bool _isDpiAware;

        public static IntVector MonitorSize
        {
            get
            {
                if (!_isDpiAware)
                {
                    SetProcessDPIAware();
                    _isDpiAware = true;
                }
                return new(GetSystemMetrics(SM_CX_SCREEN), GetSystemMetrics(SM_CY_SCREEN));
            }
        }

        public static IntVector GetWindowSize(IntPtr windowHandle)
        {
            if (!_isDpiAware)
            {
                SetProcessDPIAware();
                _isDpiAware = true;
            }
            GetWindowRect(windowHandle, out var rect);
            var width = rect.Right - rect.Left;
            var height = rect.Bottom - rect.Top;
            return new IntVector(width, height);
        }
        
        public static IntVector GetCenterOfWindow(IntVector windowSize)
        {
            var difference = MonitorSize / 2 - windowSize / 2;
            return difference;
        }
    }
}