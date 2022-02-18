using System;
using N8Engine.SceneManagement;

namespace N8Engine;

// TODO: OnQuit method to unsub from events and whatnot.
public sealed class Game : ServiceLocator<Module>, GameEvents
{
    public event Action<Frame> OnEarlyUpdate;
    public event Action<Frame> OnUpdate;
    public event Action<Frame> OnLateUpdate;
    // TODO: Maybe this gets passed into the scene manager later???
    public static readonly Modules Modules = new();
    
    readonly Action<Frame> _everyFrame;
    readonly SceneManager _sceneManager;
    Scene _firstScene = new EmptyScene();
    Loop _loop;

    public Game()
    {
        _everyFrame = frame =>
        {
            OnEarlyUpdate?.Invoke(frame);
            OnUpdate?.Invoke(frame);
            OnLateUpdate?.Invoke(frame);
        };
        _loop = new(60, _everyFrame);
        _sceneManager = new(this);
        Modules.Add(_sceneManager);
    }

    public Game WithFps(int fps)
    {
        _loop = new(fps, _everyFrame);
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
        _loop.Run();
    }
}