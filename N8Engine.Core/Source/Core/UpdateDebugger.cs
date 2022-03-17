using System;

namespace N8Engine;

public sealed class UpdateDebugger : Component
{
    readonly Action _onEarlyUpdate;
    readonly Action _onUpdate;
    readonly Action _onLateUpdate;
    
    public UpdateDebugger(Action onEarlyUpdate, Action onUpdate, Action onLateUpdate)
    {
        _onEarlyUpdate = onEarlyUpdate;
        _onUpdate = onUpdate;
        _onLateUpdate = onLateUpdate;
    }

    public override void EarlyUpdate(Frame frame) => _onEarlyUpdate?.Invoke();
    public override void Update(Frame frame) => _onUpdate?.Invoke();
    public override void LateUpdate(Frame frame) => _onLateUpdate?.Invoke();
}