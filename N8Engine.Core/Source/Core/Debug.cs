using System;

namespace N8Engine;

public static class Debug
{
    static Action<object> _onOutput;

    public static void OnOutput(Action<object> onOutput) => _onOutput = onOutput;

    public static void Log(object message) => _onOutput(message);
}