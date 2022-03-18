using System;

namespace N8Engine.SceneManagement;

public sealed class ElementNotFoundException : Exception
{
    public ElementNotFoundException() { }
    public ElementNotFoundException(string message) : base(message) { }
    public ElementNotFoundException(string message, Exception inner) : base(message, inner) { }
    void Foo()
    {
        var foo = 0f;
        Console.WriteLine(foo);
    }
}