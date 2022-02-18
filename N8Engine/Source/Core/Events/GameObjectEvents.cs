using System;

namespace N8Engine;

sealed class GameObjectEvents
{
    public event Action<Frame> OnUpdate;
    public event Action<Frame> OnLateUpdate;
    public event Action<Frame> OnPhysicsUpdate;
    
    public void Fire(Frame frame)
    {
        OnUpdate?.Invoke(frame);
        OnLateUpdate?.Invoke(frame);
        OnPhysicsUpdate?.Invoke(frame);
    }
}