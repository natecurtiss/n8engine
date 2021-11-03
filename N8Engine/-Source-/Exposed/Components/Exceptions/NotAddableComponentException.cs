using System;

namespace N8Engine
{
    sealed class NotAddableComponentException : Exception
    {
        public NotAddableComponentException() { }
        public NotAddableComponentException(string message) : base(message) { }
        public NotAddableComponentException(string message, Exception inner) : base(message, inner) { }
    }
}