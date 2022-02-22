namespace N8Engine.Mathematics;

public sealed class TrackScale : Component
{
    readonly Transform _me;
    readonly Transform _tracker;
    
    public TrackScale(Transform me, Transform tracker)
    {
        _me = me;
        _tracker = tracker;
    }

    public override void EarlyUpdate(Frame frame) => _me.Scale = _tracker.Scale;
}