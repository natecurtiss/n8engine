using System;

namespace N8Engine;

interface GameEvents
{
    event Action<Frame> OnEarlyUpdate;
    event Action<Frame> OnUpdate;
    event Action<Frame> OnLateUpdate;
}