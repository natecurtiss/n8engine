using N8Engine.Internal;

namespace N8Engine
{
    public static class Debug
    {
        private static IDebugOutput _debugOutput;
        
        internal static void Initialize() => _debugOutput = new DebugWriteToFile();

        public static void Log(in object message) => _debugOutput.Write(message.ToString());
    }
}