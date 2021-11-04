using System;

namespace N8Engine.Loop
{
    interface ILoopEvents
    {
        event Action OnStart;
        event Action<float> OnUpdate;
    }
}