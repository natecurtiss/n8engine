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
            var pathToLogsFolder = $"{PathExtensions.PathToRootFolder}\\.error_logs";
            File.WriteAllText(pathToLogsFolder, string.Empty);
            
            RedirectToFile(pathToLogsFolder);
            var processStartInfo = new ProcessStartInfo($"{PathExtensions.PathToRootFolder}\\ErrorConsole.exe")
            {
                WindowStyle = ProcessWindowStyle.Normal,
                UseShellExecute = true,
                CreateNoWindow = false,
                Arguments = pathToLogsFolder
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