using System;

namespace N8Engine.Internal
{
    interface IInternalEvents
    {
        event Action OnInternalStart;
        event Action OnInternalPreUpdate;
    }
}