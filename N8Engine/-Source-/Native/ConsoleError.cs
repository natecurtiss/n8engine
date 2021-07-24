using System;
using System.IO;
using System.Runtime.InteropServices;

namespace N8Engine.Native
{
    internal static class ConsoleError
    {
        [DllImport("Kernel32.dll", SetLastError = true) ]
        private static extern int SetStdHandle(int device, IntPtr handle); 
        
        public static void RedirectToFile(string path)
        {
            var fileStream = new FileStream(path, FileMode.Open);
            var handle = fileStream.Handle;
            SetStdHandle(ConsoleWindow.STANDARD_ERROR_HANDLE_NUMBER, handle);
        }
    }
}