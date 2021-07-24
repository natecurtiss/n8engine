using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using N8Engine.Native;
using Shared;

namespace N8Engine.Debugging
{
    internal sealed class DebugConsole : IDebugOutput
    {
        private readonly string _pathToLogsFolder = PathExtensions.PathToLogsFolder;
        
        public DebugConsole()
        {
            ConsoleError.RedirectToFile(_pathToLogsFolder);
            throw new Exception("wow");
            var processStartInfo = new ProcessStartInfo($"{PathExtensions.PathToRootFolder}\\DebugConsole\\bin\\Release\\net5.0\\DebugConsole.exe")
            {
                WindowStyle = ProcessWindowStyle.Normal, 
                UseShellExecute = true,
                CreateNoWindow = false
            };
            Process.Start(processStartInfo);
        }

        public void Write(string message)
        {
            return;
            using var fileStream = new FileStream(_pathToLogsFolder, FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
            using var streamWriter = new StreamWriter(fileStream);
            streamWriter.WriteLine(message);
        }
    }
}