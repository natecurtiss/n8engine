namespace N8Engine.Events;

sealed class GameObjectEvents
{
    public readonly Event<Frame> OnEarlyUpdate = new();
    public readonly Event<Frame> OnUpdate = new();
    public readonly Event<Frame> OnLateUpdate = new();
}