namespace N8Engine;

public sealed class MissingModuleException : Exception
{
    public MissingModuleException() { }
    public MissingModuleException(string message) : base(message) { }
    public MissingModuleException(string message, Exception inner) : base(message, inner) { }
}