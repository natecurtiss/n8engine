using System;
using System.Diagnostics;
using System.IO;

namespace N8Engine.Debugging
{
    [Obsolete]
    internal sealed class DebugConsole : IDebugOutput
    {
        private readonly StreamWriter _standardOutput;
        private readonly StreamReader _standardInput;
        
        public DebugConsole()
        {
            var processStartInfo = new ProcessStartInfo("cmd.exe")
            {
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
            };
            var process = Process.Start(processStartInfo);
                    
            _standardOutput = process?.StandardInput;
            _standardInput = process?.StandardOutput;
        }

        public void Write(in string message)
        {
            _standardOutput.WriteLine("Hello world!");
            _standardInput.Close();
        }
    }
}