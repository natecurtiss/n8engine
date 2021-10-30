using System;
using System.Runtime.InteropServices;
using N8Engine.Mathematics;
using static N8Engine.External.CommonStructures;

namespace N8Engine.External.User
{
    static class UserMetrics
    {
        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowrect
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getsystemmetrics?redirectedfrom=MSDN
        [DllImport("user32.dll")]
        static extern int GetSystemMetrics(int nIndex);
        
        const int SM_CX_SCREEN = 0;
        const int SM_CY_SCREEN = 1;

        public static IntVector MonitorSize => new(GetSystemMetrics(SM_CX_SCREEN), GetSystemMetrics(SM_CY_SCREEN));
        
        public static IntVector GetWindowSize(IntPtr windowHandle)
        {
            GetWindowRect(windowHandle, out var rect);
            var width = rect.BottomRightX - rect.TopLeftX;
            var height = rect.TopLeftY - rect.BottomRightY;
            return new IntVector(width, height);
        }
        
        public static IntVector GetCenterOfWindow(IntVector windowSize)
        {
            var monitorSize = MonitorSize;
            var difference = monitorSize - windowSize;
            return difference / 2;
        }
    }
}