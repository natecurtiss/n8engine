using System;
using N8Engine.Windowing;

namespace N8Engine.SceneManagement;

public sealed class SceneManager : GameModule
{
    readonly Loop _loop;
    readonly WindowEvents _windowEvents;
    readonly Action<SceneModules> _onAddSceneModules;
    readonly Action<SceneModules> _onRemoveSceneModules;

    public Scene CurrentScene { get; private set; } = new EmptyScene();
    
    internal SceneManager(Loop loop, WindowEvents windowEvents, Action<SceneModules> onAddSceneModules, Action<SceneModules> onRemoveSceneModules)
    {
        _loop = loop;
        _windowEvents = windowEvents;
        _onAddSceneModules = onAddSceneModules;
        _onRemoveSceneModules = onRemoveSceneModules;
    }

    public void Load(Scene scene)
    {
        _loop.OnStart -= CurrentScene.Start;
        _loop.OnUpdate -= CurrentScene.Update;
        _loop.OnRender -= CurrentScene.Render;
        _windowEvents.OnClose -= UnloadCurrentScene;
        UnloadCurrentScene();
        CurrentScene = scene;
        
        _loop.OnStart += CurrentScene.Start;
        _loop.OnUpdate += CurrentScene.Update;
        _loop.OnRender += CurrentScene.Render;
        _windowEvents.OnClose += UnloadCurrentScene;
        CurrentScene.SwitchTo(_onAddSceneModules);
    }

    void UnloadCurrentScene() => CurrentScene.SwitchFrom(_onRemoveSceneModules);
}