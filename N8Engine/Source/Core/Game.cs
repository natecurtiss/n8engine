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
            _gameObjectEvents.Fire(frame);
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