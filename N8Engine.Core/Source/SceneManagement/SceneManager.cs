using N8Engine.Windowing;

namespace N8Engine.SceneManagement;

public sealed class SceneManager : Module
{
    readonly WindowSize _windowSize;
    readonly Loop _loop;

    public Scene CurrentScene { get; private set; } = new EmptyScene();
    
    internal SceneManager(Loop loop, WindowSize windowSize)
    {
        _loop = loop;
        _windowSize = windowSize;
    }

    public void Load(Scene scene)
    {
        _loop.OnUpdate -= CurrentScene.Update;
        _loop.OnRender -= CurrentScene.Render;
        CurrentScene.Unload();
        CurrentScene = scene;
        
        _loop.OnUpdate += CurrentScene.Update;
        _loop.OnRender += CurrentScene.Render;
        CurrentScene.SwitchTo(_windowSize);
        CurrentScene.Load();
    }
}