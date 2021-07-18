using System;
using System.Diagnostics;
using System.IO;

namespace N8Engine.Debugging
{
    internal sealed class DebugWriteToFile : IDebugOutput
    {
        private readonly TextWriterTraceListener _textWriterTraceListener;

        private String _cachedPath;
        private String DebugPath
        {
            get
            {
                if (!String.IsNullOrEmpty(_cachedPath)) return _cachedPath;
                
                String __basePath = AppDomain.CurrentDomain.BaseDirectory;
                return _cachedPath = Path.Combine(__basePath, "debug.txt");
            }
        }

        public DebugWriteToFile()
        {
            File.WriteAllText(path: DebugPath, contents: String.Empty);
            FileStream __traceLog = new(DebugPath, FileMode.OpenOrCreate);
            _textWriterTraceListener = new TextWriterTraceListener(__traceLog);
        }

        public void Write(in String message)
        {
            _textWriterTraceListener.WriteLine(message);
            _textWriterTraceListener.Flush();
        }
    }
}