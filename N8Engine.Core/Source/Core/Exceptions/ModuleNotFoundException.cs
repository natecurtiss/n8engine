using System;

namespace N8Engine;

public sealed class ModuleNotFoundException : Exception
{
    public ModuleNotFoundException() { }
    public ModuleNotFoundException(string message) : base(message) { }
    public ModuleNotFoundException(string message, Exception inner) : base(message, inner) { }
}