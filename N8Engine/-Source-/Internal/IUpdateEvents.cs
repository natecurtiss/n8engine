using System;

namespace N8Engine.Internal
{
    interface IUpdateEvents
    {
        event Action<float> OnUpdate;
        event Action<float> OnPhysicsUpdate;
        event Action<float> OnLateUpdate;
    }
}