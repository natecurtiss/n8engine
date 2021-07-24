using System;
using System.Runtime.InteropServices;

namespace N8Engine.Native
{
    internal static class ConsoleWindow
    {
        /// <summary>
        /// Returns a handle to to the specified standard device (input, output, or error).
        /// </summary>
        /// <param name="nStdHandle"> The number of the standard device. </param>
        /// <returns> A handle to to the specified standard device (input, output, or error). </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        public static readonly IntPtr StandardInputHandle = GetStdHandle(-10);
        public static readonly IntPtr StandardOutputHandle = GetStdHandle(-11);
        public static readonly IntPtr StandardErrorHandle = GetStdHandle(-12);
    }
}