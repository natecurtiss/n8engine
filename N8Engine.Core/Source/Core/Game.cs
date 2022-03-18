using System;
using N8Engine.InputSystem;
using N8Engine.Rendering;
using N8Engine.SceneManagement;
using N8Engine.Windowing;

namespace N8Engine;

public sealed class Game : Loop, Ticks
{
    public event Action OnStart;
    public event Action<Frame> OnUpdate;
    public event Action OnRender;
    
    WindowOptions _windowOptions;
    Scene _firstScene = new EmptyScene();

    public Game() => _windowOptions = new("N8Engine Game", 1280, 720, 60, WindowState.Windowed, true);
    
    public Game WithWindowTitle(string title)
    {
        _windowOptions = _windowOptions.WithTitle(title);
        return this;
    }

    public Game WithWindowSize(uint width, uint height)
    {
        _windowOptions = _windowOptions.WithSize(width, height);
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
    
    public Game Maximized()
    {
        _windowOptions = _windowOptions.Maximized();
        return this;
    }
    
    public Game Windowed()
    {
        _windowOptions = _windowOptions.Windowed();
        return this;
    }

    public Game WithResizableWindow()
    {
        _windowOptions = _windowOptions.Resizable();
        return this;
    }
    
    public Game WithNotResizableWindow()
    {
        _windowOptions = _windowOptions.NotResizable();
        return this;
    }

    public Game WithFirstScene(Scene scene)
    {
        _firstScene = scene;
        return this;
    }

    public Game WithDebugOutput(Action<object> onOutput)
    {
        Debug.OnOutput(onOutput);
        return this;
    }

    public void Run() => Run(GameStart.Default);
    internal void Run(GameStart starter) => starter.Start(this, this, _windowOptions, _firstScene);

    void Ticks.Start() => OnStart?.Invoke();
    void Ticks.Update(Frame frame) => OnUpdate?.Invoke(frame);
    void Ticks.Render() => OnRender?.Invoke();
}