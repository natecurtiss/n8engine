using SysDebug = System.Diagnostics.Debug;

namespace N8Engine.Debugging
{
    sealed class DebugConsoleLogger : ILogger
    {
        void ILogger.Write(string message) => SysDebug.WriteLine(message);
    }
}