using System;
using System.Numerics;
using N8Engine.InputSystem;
using N8Engine.Rendering;
using N8Engine.SceneManagement;
using N8Engine.Windowing;
using Silk.NET.OpenGL;

namespace N8Engine;

public sealed class Game : ServiceLocator<Module>, Loop
{
    public static readonly Modules Modules = new();
    public event Action<Frame> OnUpdate;
    public event Action<GL> OnRender;

    WindowOptions _windowOptions;
    Window _window;

    readonly Debug _debug = new();
    readonly Input _input = new();
    readonly SceneManager _sceneManager;
    Camera _camera;
    Scene _firstScene = new EmptyScene();

    public Game()
    {
        _windowOptions = new("N8Engine Game", new(1280, 720), 60, WindowState.Windowed);
        Modules.Add(_debug);
        Modules.Add(_input);
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
        _window.OnLoad += () => _sceneManager.Load(_firstScene);
        _window.OnUpdate += frame => OnUpdate?.Invoke(frame);
        _window.OnRender += () => OnRender?.Invoke(_window.GL);
        _window.OnKeyDown += key => _input.UpdateKey(key, true);
        _window.OnKeyUp += key => _input.UpdateKey(key, false);
        
        _camera = new(Vector2.Zero, 1f, _window);
        Modules.Add(_camera);

        _window.Run();
    }
}