namespace N8Engine;

public sealed class TrackRotation : Component
{
    readonly Transform _me;
    readonly Transform _tracker;
    
    public TrackRotation(Transform me, Transform tracker)
    {
        _me = me;
        _tracker = tracker;
    }

    public override void EarlyUpdate(Frame frame) => _me.Rotation = _tracker.Rotation;
}