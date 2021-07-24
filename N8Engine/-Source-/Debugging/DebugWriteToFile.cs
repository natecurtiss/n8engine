using System.Diagnostics;
using System.IO;
using Shared;

namespace N8Engine.Debugging
{
    internal sealed class DebugWriteToFile : IDebugOutput
    {
        private readonly TextWriterTraceListener _textWriterTraceListener;

        public DebugWriteToFile()
        {
            var path = $"{PathExtensions.PathToLogsFolder}\\N8Engine\\-Source-\\Debugging\\debug.txt";
            File.WriteAllText(path, string.Empty);
            FileStream traceLog = new(path, FileMode.OpenOrCreate);
            _textWriterTraceListener = new TextWriterTraceListener(traceLog);
        }

        public void Write(string message)
        {
            _textWriterTraceListener.WriteLine(message);
            _textWriterTraceListener.Flush();
        }
    }
}