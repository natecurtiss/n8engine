using System;
using System.Runtime.InteropServices;
using N8Engine.Mathematics;
using static N8Engine.External.CommonStructures;

namespace N8Engine.External.Console
{
    static class ConsoleInfo
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

    }
}