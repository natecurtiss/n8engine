using System;

namespace N8Engine.SceneManagement;

public sealed class CogNotFoundException : Exception
{
    public CogNotFoundException() { }
    public CogNotFoundException(string message) : base(message) { }
    public CogNotFoundException(string message, Exception inner) : base(message, inner) { }
}