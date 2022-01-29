namespace N8Engine;

public sealed class Application
{
    readonly Loop _loop;
    readonly Window _window;
    
    Application(int targetFps, uint width, uint height, string title)
    {
        _loop = new(targetFps, Update);
        _window = new(width, height, title);
    }

    public static Application WithWindow(int fps, uint width, uint height, string title) => new(fps, width, height, title);

    public void Run() => _loop.Start();

    void Update(Frame frame)
    {
        Modules.Update(frame);
        _window.Write();
    }
}