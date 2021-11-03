using System;

namespace N8Engine
{
    sealed class ComponentIsNotAttachedException : Exception
    {
        public ComponentIsNotAttachedException() { }
        public ComponentIsNotAttachedException(string message) : base(message) { }
        public ComponentIsNotAttachedException(string message, Exception inner) : base(message, inner) { }
    }
}