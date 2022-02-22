using System;

namespace N8Engine;

public class ComponentNotAttachedException : Exception
{
    public ComponentNotAttachedException() { }
    public ComponentNotAttachedException(string message) : base(message) { }
    public ComponentNotAttachedException(string message, Exception inner) : base(message, inner) { }
}