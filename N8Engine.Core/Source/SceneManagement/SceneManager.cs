using N8Engine.Windowing;

namespace N8Engine.SceneManagement;

public sealed class SceneManager : Module
{
    readonly Loop _loop;
    readonly WindowSize _windowSize;
    readonly WindowRendering _rendering;

    public Scene CurrentScene { get; private set; } = new EmptyScene();
    
    internal SceneManager(Loop loop, WindowSize windowSize, WindowRendering rendering)
    {
        _loop = loop;
        _windowSize = windowSize;
        _rendering = rendering;
    }

    public void Load(Scene scene)
    {
        _loop.OnUpdate -= CurrentScene.Update;
        _loop.OnRender -= CurrentScene.Render;
        CurrentScene.Unload();
        CurrentScene = scene;
        
        _loop.OnUpdate += CurrentScene.Update;
        _loop.OnRender += CurrentScene.Render;
        CurrentScene.SwitchTo(_windowSize, _rendering);
        CurrentScene.Load();
    }
}