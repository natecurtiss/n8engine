using System;

namespace N8Engine;

public sealed class ComponentAlreadyAttachedException : Exception
{
    public ComponentAlreadyAttachedException() { }
    public ComponentAlreadyAttachedException(string message) : base(message) { }
    public ComponentAlreadyAttachedException(string message, Exception inner) : base(message, inner) { }
}