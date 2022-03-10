using System;

namespace N8Engine.Rendering;

public sealed class ShaderFailedCompilationException : Exception
{
    public ShaderFailedCompilationException() { }
    public ShaderFailedCompilationException(string message) : base(message) { }
    public ShaderFailedCompilationException(string message, Exception inner) : base(message, inner) { }
}