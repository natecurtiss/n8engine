using System;

namespace N8Engine;

public sealed class GameObjectIsDestroyedException : Exception
{
    public GameObjectIsDestroyedException() { }
    public GameObjectIsDestroyedException(string message) : base(message) { }
    public GameObjectIsDestroyedException(string message, Exception inner) : base(message, inner) { }
}