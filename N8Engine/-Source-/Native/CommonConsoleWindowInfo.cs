using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace N8Engine.Native
{
    static class CommonConsoleWindowInfo
    {
        // https://docs.microsoft.com/en-us/windows/console/getstdhandle
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);
        
        // https://docs.microsoft.com/en-us/windows/console/getconsolewindow
        [DllImport("kernel32.dll", ExactSpelling = true)]
        static extern IntPtr GetConsoleWindow();
        
        const int STANDARD_INPUT_HANDLE_NUMBER = -10;
        const int STANDARD_OUTPUT_HANDLE_NUMBER = -11;
        const int STANDARD_ERROR_HANDLE_NUMBER = -12;
        
        public static readonly IntPtr StandardInputHandle = GetStdHandle(STANDARD_INPUT_HANDLE_NUMBER);
        public static readonly IntPtr StandardOutputHandle = GetStdHandle(STANDARD_OUTPUT_HANDLE_NUMBER);
        public static readonly IntPtr StandardErrorHandle = GetStdHandle(STANDARD_ERROR_HANDLE_NUMBER);

        public static readonly IntPtr Handle = GetConsoleWindow();
        
        [StructLayout(LayoutKind.Sequential)]
        public struct Coordinate
        {
            public short X;
            public short Y;
            
            public Coordinate(short x, short y) 
            { 
                X = x;
                Y = y;
            }
        }
    }
}