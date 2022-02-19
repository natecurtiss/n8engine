using System;
using N8Engine.SceneManagement;
using N8Engine.Windowing;

namespace N8Engine;

public sealed class Game : ServiceLocator<Module>, GameEvents
{
    public event Action<Frame> OnEarlyUpdate;
    public event Action<Frame> OnUpdate;
    public event Action<Frame> OnLateUpdate;
    public static readonly Modules Modules = new();

    readonly Action<Frame> _everyFrame;
    WindowOptions _windowOptions;
    Window _window;
    
    readonly SceneManager _sceneManager;
    Scene _firstScene = new EmptyScene();

    public Game()
    {
        _everyFrame = frame =>
        {
            OnEarlyUpdate?.Invoke(frame);
            OnUpdate?.Invoke(frame);
            OnLateUpdate?.Invoke(frame);
        };
        _windowOptions = new("N8Engine Game", new(1280, 720), 60, false);
        
        _sceneManager = new(this);
        Modules.Add(_sceneManager);
    }

    public Game WithWindowTitle(string title)
    {
        _windowOptions = _windowOptions.WithTitle(title);
        return this;
    }

    public Game WithWindowSize(uint width, uint height)
    {
        _windowOptions = _windowOptions.WithSize(new(width, height));
        return this;
    }

    public Game WithFps(int fps)
    {
        _windowOptions = _windowOptions.WithFps(fps);
        return this;
    }

    public Game Fullscreen()
    {
        _windowOptions = _windowOptions.Fullscreen();
        return this;
    }
    
    public Game Windowed()
    {
        _windowOptions = _windowOptions.Windowed();
        return this;
    }

    public Game WithFirstScene(Scene scene)
    {
        _firstScene = scene;
        return this;
    }
    
    public void Start()
    {
        _sceneManager.Load(_firstScene);
        _window = new(_windowOptions);
        _window.Run();
    }
}