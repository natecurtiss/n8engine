using System;
using System.Diagnostics;
using System.IO;

namespace N8Engine.Internal
{
    [Obsolete]
    internal sealed class DebugConsole : IDebugOutput
    {
        private readonly StreamWriter _standardOutput;
        private readonly StreamReader _standardInput;
        
        public DebugConsole()
        {
            ProcessStartInfo __processStartInfo = new("cmd.exe")
            {
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
            };
            Process __process = Process.Start(__processStartInfo);
                    
            _standardOutput = __process?.StandardInput;
            _standardInput = __process?.StandardOutput;
        }

        public void Write(in string message)
        {
            _standardOutput.WriteLine("Hello world!");
            _standardInput.Close();
        }
    }
}