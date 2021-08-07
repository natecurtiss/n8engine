using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace N8Engine.Native
{
    internal static class ConsoleErrorHelper
    {
        // https://docs.microsoft.com/en-us/windows/console/setstdhandle
        [DllImport("Kernel32.dll", SetLastError = true) ]
        private static extern int SetStdHandle(int device, IntPtr handle);
        
        public static void CreateErrorConsole()
        {
            var pathToLogsFolder = PathExtensions.PathToLogsFolder;
            File.WriteAllText(pathToLogsFolder, string.Empty);
            
            RedirectToFile(pathToLogsFolder);
            
            var processStartInfo = new ProcessStartInfo($"{PathExtensions.PathToRootFolder}\\DebugConsole\\bin\\Release\\net5.0\\DebugConsole.exe")
            {
                WindowStyle = ProcessWindowStyle.Normal,
                UseShellExecute = true,
                CreateNoWindow = false
            };
            Process.Start(processStartInfo);
        }
        
        private static void RedirectToFile(string path)
        {
            var fileStream = new FileStream(path, FileMode.Open);
            var handle = fileStream.Handle;
            SetStdHandle(CommonConsoleWindowInfo.STANDARD_ERROR_HANDLE_NUMBER, handle);
        }
    }
}