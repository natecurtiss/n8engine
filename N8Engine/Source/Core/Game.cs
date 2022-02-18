using System;
using N8Engine.SceneManagement;

namespace N8Engine;

public sealed class Game : ServiceLocator<Module>, GameEvents
{
    public event Action<Frame> OnEarlyUpdate;
    public event Action<Frame> OnUpdate;
    public event Action<Frame> OnLateUpdate;
    public static readonly Modules Modules = new();

    Loop _loop;

    public Game()
    {
        _loop = new(60, frame =>
        {
            OnEarlyUpdate?.Invoke(frame);
            OnUpdate?.Invoke(frame);
            OnLateUpdate?.Invoke(frame);
        });
    }

    public Game WithFps(int fps)
    {
        _loop = new(fps);
        return this;
    }

    public Game WithFirstScene(Scene scene)
    {
        return this;
    }
    
    public void Start()
    {
        _loop.Run();
    }
}