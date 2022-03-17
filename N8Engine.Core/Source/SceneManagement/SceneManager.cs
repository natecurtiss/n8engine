using System;
using N8Engine.Windowing;

namespace N8Engine.SceneManagement;

public sealed class SceneManager : Module
{
    readonly Loop _loop;
    readonly WindowEvents _windowEvents;
    readonly Action<Cogs> _onAddCogs;
    readonly Action<Cogs> _onRemoveCogs;

    public Scene CurrentScene { get; private set; } = new EmptyScene();
    
    internal SceneManager(Loop loop, WindowEvents windowEvents, Action<Cogs> onAddCogs, Action<Cogs> onRemoveCogs)
    {
        _loop = loop;
        _windowEvents = windowEvents;
        _onAddCogs = onAddCogs;
        _onRemoveCogs = onRemoveCogs;
        CurrentScene.SwitchTo(onAddCogs);
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
        CurrentScene.SwitchTo(_onAddCogs);
    }

    void UnloadCurrentScene() => CurrentScene.SwitchFrom(_onRemoveCogs);
}