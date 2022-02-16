namespace N8Engine;

public sealed class Game : ServiceLocator<Module>
{
    public static readonly Modules Modules = new();
    Loop _loop;

    public Game()
    {
        _loop = new(60);
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