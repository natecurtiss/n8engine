using System;

namespace N8Engine
{
    sealed class ComponentIsNotFoundException : Exception
    {
        public ComponentIsNotFoundException() { }
        public ComponentIsNotFoundException(string message) : base(message) { }
        public ComponentIsNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}