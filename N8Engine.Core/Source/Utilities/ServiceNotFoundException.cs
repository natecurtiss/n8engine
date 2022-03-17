using System;

namespace N8Engine;

public sealed class ServiceNotFoundException : Exception
{
    public ServiceNotFoundException() { }
    public ServiceNotFoundException(string message) : base(message) { }
    public ServiceNotFoundException(string message, Exception inner) : base(message, inner) { }
}