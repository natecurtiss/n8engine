using System.Diagnostics;
using System.IO;

namespace N8Engine.Debugging
{
    internal sealed class DebugWriteToFile : IDebugOutput
    {
        private readonly TextWriterTraceListener _textWriterTraceListener;

        public DebugWriteToFile()
        {
            const string path = @"C:\Users\NateDawg\RiderProjects\N8Engine\N8Engine\-Source-\Temporary\debug.txt";
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