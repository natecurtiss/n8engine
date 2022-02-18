using N8Engine.Events;

namespace N8Engine;

public sealed class Game : ServiceLocator<Module>
{
    public static readonly Modules Modules = new();
    static readonly GameObjectEvents _gameObjectEvents = new();
    
    Loop _loop;

    public Game()
    {
        _loop = new(60, frame =>
        {
            _gameObjectEvents.OnEarlyUpdate.Raise(frame);
            _gameObjectEvents.OnUpdate.Raise(frame);
            _gameObjectEvents.OnLateUpdate.Raise(frame);
        });
    }

    public Game WithFps(int fps)
    {
        _loop = new(fps);
        return this;
    }
    
    public void Start()
    {
        _loop.Run();
    }
}