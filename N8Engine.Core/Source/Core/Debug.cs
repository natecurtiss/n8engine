using System;

namespace N8Engine;

public sealed class Debug : GameModule
{
    Action<object> _onOutput;
    public Debug(Action<object> onOutput) => OnOutput(onOutput);
    public void OnOutput(Action<object> onOutput) => _onOutput = onOutput;
    public void Log(object message) => _onOutput(message);
}