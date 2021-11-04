using System;

namespace N8Engine.Internal
{
    sealed class InternalEvents
    {
        public event Action OnPreUpdate;

        public void Invoke() => OnPreUpdate?.Invoke();
    }
}