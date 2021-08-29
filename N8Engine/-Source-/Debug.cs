﻿using System;

namespace N8Engine
{
    public static class Debug
    {
        static Action<string> _onDebugLog;

        internal static void Initialize(Launcher launcher) => _onDebugLog = launcher.OnDebugLog;
        
#nullable enable
        public static void Log(object? message)
        {
            if (message != null)
                _onDebugLog.Invoke(message.ToString());
        }
#nullable disable
    }
}
