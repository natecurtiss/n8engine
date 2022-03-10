using System;

namespace N8Engine;

public sealed class SceneModuleNotFoundException : Exception
{
    public SceneModuleNotFoundException() { }
    public SceneModuleNotFoundException(string message) : base(message) { }
    public SceneModuleNotFoundException(string message, Exception inner) : base(message, inner) { }
}