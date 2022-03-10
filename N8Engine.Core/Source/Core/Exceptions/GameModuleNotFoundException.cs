using System;

namespace N8Engine;

public sealed class GameModuleNotFoundException : Exception
{
    public GameModuleNotFoundException() { }
    public GameModuleNotFoundException(string message) : base(message) { }
    public GameModuleNotFoundException(string message, Exception inner) : base(message, inner) { }
}