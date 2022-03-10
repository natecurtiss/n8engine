using System;
using N8Engine.InputSystem;
using N8Engine.Rendering;
using N8Engine.SceneManagement;
using N8Engine.Windowing;

namespace N8Engine;

public sealed class Game : Loop
{
    public static readonly GameModules Modules = new();

    public event Action OnStart;
    public event Action<Frame> OnUpdate;
    public event Action OnRender;

    Window _window;
    Debug _debug;
    Input _input;
    SceneManager _sceneManager;
    
    WindowOptions _windowOptions;
    Scene _firstScene = new EmptyScene();

    public Game() => _windowOptions = new("N8Engine Game", 1280, 720, 60, WindowState.Windowed);

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

    public Game WithFirstScene(Scene scene)
    {
        _firstScene = scene;
        return this;
    }

    public Game WithDebugOutput(Action<object> onOutput)
    {
        _debug.OnOutput(onOutput);
        return this;
    }
    
    public void Start()
    {
        _window = new(_windowOptions);
        _window.OnLoad += () =>
        {
            Modules.Add(_debug = new(Console.WriteLine));
            Modules.Add(_input = new());
            Modules.Add<Graphics>(_window.CreateGL());
            Modules.Add(_sceneManager = new(this, _window, _window));
            _sceneManager.Load(_firstScene);
            OnStart?.Invoke();
        };
        _window.OnUpdate += frame => OnUpdate?.Invoke(frame);
        _window.OnRender += () => OnRender?.Invoke();
        _window.OnKeyDown += key => _input.UpdateKey(key, true);
        _window.OnKeyUp += key => _input.UpdateKey(key, false);
        _window.Run();
    }
}