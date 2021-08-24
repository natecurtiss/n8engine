using System;

namespace N8Engine
{
    public static class Debug
    {
        private static Action<string> _onDebugLog;

        internal static void Initialize(Launcher launcher) => _onDebugLog = launcher.OnDebugLog;

        public static void Log(object message) => _onDebugLog.Invoke(message.ToString());
    }
}
