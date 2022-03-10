using System;

namespace N8Engine.Rendering;

public sealed class MissingUniformOnShaderException : Exception
{
    public MissingUniformOnShaderException() { }
    public MissingUniformOnShaderException(string message) : base(message) { }
    public MissingUniformOnShaderException(string message, Exception inner) : base(message, inner) { }
}