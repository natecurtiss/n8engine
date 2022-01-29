namespace N8Engine;

public sealed class Application
{
    Loop _loop;
    Window _window;
    World _world;

    Application() { }

    public static Application New() => new Application()
        .WithFps(60)
        .WithFloatingWindow(1280, 720, "Game")
        .WithLevels(new EmptyLevel());
    
    public Application WithFps(int fps)
    {
        _loop = new(fps, Update);
        return this;
    }
    
    public Application WithFloatingWindow(uint width, uint height, string title)
    {
        _window = new FloatingWindow(width, height, title);
        return this;
    }
    
    public Application WithFullscreenWindow(uint width, uint height, string title)
    {
        _window = new FullscreenWindow(width, height, title);
        return this;
    }

    public Application WithLevels(params Level[] levels)
    {
        _world = new(levels);
        return this;
    }

    public void Run() => _loop.Start();

    void Update(Frame frame)
    {
        _world.Update(frame);
        _window.Write();
    }
}