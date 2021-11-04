using System;

namespace N8Engine
{
    sealed class CannotHaveMultipleComponentException : Exception
    {
        public CannotHaveMultipleComponentException() { }
        public CannotHaveMultipleComponentException(string message) : base(message) { }
        public CannotHaveMultipleComponentException(string message, Exception inner) : base(message, inner) { }
    }
}