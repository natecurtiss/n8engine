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

    public override void EarlyUpdate() => _onEarlyUpdate?.Invoke();
    public override void Update() => _onUpdate?.Invoke();
    public override void LateUpdate() => _onLateUpdate?.Invoke();
}