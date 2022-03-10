using N8Engine.Windowing;

namespace N8Engine.SceneManagement;

public sealed class SceneManager : GameModule
{
    readonly Loop _loop;
    readonly WindowSize _windowSize;
    readonly WindowEvents _windowEvents;

    public Scene CurrentScene { get; private set; } = new EmptyScene();
    
    internal SceneManager(Loop loop, WindowSize windowSize, WindowEvents windowEvents)
    {
        _loop = loop;
        _windowSize = windowSize;
        _windowEvents = windowEvents;
    }

    public void Load(Scene scene)
    {
        _loop.OnStart -= CurrentScene.Start;
        _loop.OnUpdate -= CurrentScene.Update;
        _loop.OnRender -= CurrentScene.Render;
        _windowEvents.OnClose -= CurrentScene.Unload;
        CurrentScene.Unload();
        CurrentScene = scene;
        
        _loop.OnStart += CurrentScene.Start;
        _loop.OnUpdate += CurrentScene.Update;
        _loop.OnRender += CurrentScene.Render;
        _windowEvents.OnClose += CurrentScene.Unload;
        CurrentScene.SwitchTo();
    }
}