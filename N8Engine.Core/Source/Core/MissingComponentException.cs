using System;

namespace N8Engine;

public class MissingComponentException : Exception
{
    public MissingComponentException() { }
    public MissingComponentException(string message) : base(message) { }
    public MissingComponentException(string message, Exception inner) : base(message, inner) { }
}