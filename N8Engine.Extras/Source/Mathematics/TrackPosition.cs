namespace N8Engine.Mathematics;

public sealed class TrackPosition : Component
{
    readonly Transform _me;
    readonly Transform _tracker;
    
    public TrackPosition(Transform me, Transform tracker)
    {
        _me = me;
        _tracker = tracker;
    }

    public override void EarlyUpdate(Frame frame) => _me.Position = _tracker.Position;
}