using System;
using System.Runtime.InteropServices;
using N8Engine.Mathematics;

namespace N8Engine.External
{
    internal static class CommonConsoleWindowInfo
    {
        // https://docs.microsoft.com/en-us/windows/console/getstdhandle
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);
        
        // https://docs.microsoft.com/en-us/windows/console/getconsolewindow
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        
       // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowrect
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int nIndex);

        private const int STANDARD_INPUT_HANDLE_NUMBER = -10;
        private const int STANDARD_OUTPUT_HANDLE_NUMBER = -11;
        private const int STANDARD_ERROR_HANDLE_NUMBER = -12;

        private const int SM_CX_SCREEN = 0;
        private const int SM_CY_SCREEN = 1;
        
        public static readonly IntPtr StandardInputHandle = GetStdHandle(STANDARD_INPUT_HANDLE_NUMBER);
        public static readonly IntPtr StandardOutputHandle = GetStdHandle(STANDARD_OUTPUT_HANDLE_NUMBER);
        public static readonly IntPtr StandardErrorHandle = GetStdHandle(STANDARD_ERROR_HANDLE_NUMBER);
        public static readonly IntPtr Handle = GetConsoleWindow();

        public static IntegerVector CenterOfScreenFromWindowSize(IntegerVector windowSize)
        {
            var monitorSize = MonitorSize;
            var difference = monitorSize - windowSize;
            return difference / 2;
        }
        public static IntegerVector WindowSize
        {
            get
            {
                GetWindowRect(Handle, out var rectangle);
                var width = rectangle.Right - rectangle.Left;
                var height = rectangle.Top - rectangle.Bottom;
                return new IntegerVector(width, height);
            }
        }
        public static IntegerVector MonitorSize => new(GetSystemMetrics(SM_CX_SCREEN), GetSystemMetrics(SM_CY_SCREEN));

        [StructLayout(LayoutKind.Sequential)]
        public struct COORD
        {
            public short X;
            public short Y;
            
            public COORD(short x, short y) 
            { 
                X = x;
                Y = y;
            }
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // X position of upper-left corner.
            public int Top;         // Y position of upper-left corner.
            public int Right;       // X position of lower-right corner.
            public int Bottom;      // Y position of lower-right corner.
        }
    }
}