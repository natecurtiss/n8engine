using System;
using System.Diagnostics;
using System.IO;
using N8Engine.Native;
using Shared;

namespace N8Engine.Debugging
{
    internal sealed class DebugConsole : IDebugOutput
    {
        private readonly string _pathToLogsFolder = PathExtensions.PathToLogsFolder;
        
        public DebugConsole()
        {
            File.WriteAllText(_pathToLogsFolder, string.Empty);
            ConsoleError.RedirectToFile(_pathToLogsFolder);
            var processStartInfo = new ProcessStartInfo($"{PathExtensions.PathToRootFolder}\\DebugConsole\\bin\\Release\\net5.0\\DebugConsole.exe")
            {
                WindowStyle = ProcessWindowStyle.Normal, 
                UseShellExecute = true,
                CreateNoWindow = false
            };
            Process.Start(processStartInfo);
        }

        public void Write(string message) => Console.Error.WriteLine(message);
    }
}