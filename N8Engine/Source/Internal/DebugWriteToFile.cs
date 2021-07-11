using System.Diagnostics;
using System.IO;

namespace N8Engine.Internal
{
    internal sealed class DebugWriteToFile : IDebugOutput
    {
        private readonly TextWriterTraceListener _textWriterTraceListener;

        public DebugWriteToFile()
        {
            const string __path = @"C:\Users\NateDawg\RiderProjects\N8Engine\N8Engine\Source\Temporary\debug.txt";
            File.WriteAllText(__path, string.Empty);
            FileStream __traceLog = new(__path, FileMode.OpenOrCreate);
            _textWriterTraceListener = new TextWriterTraceListener(__traceLog);
        }

        public void Write(in string message)
        {
            _textWriterTraceListener.WriteLine(message);
            _textWriterTraceListener.Flush();
        }
    }
}