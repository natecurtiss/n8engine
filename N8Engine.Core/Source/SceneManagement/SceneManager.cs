﻿namespace N8Engine.SceneManagement;

public sealed class SceneManager : Module
{
    readonly GameEvents _events;
    public Scene CurrentScene { get; private set; }
    
    internal SceneManager(GameEvents events) => _events = events;

    public void Load(Scene scene)
    {
        if (CurrentScene != null)
        {
            UnsubscribeFromEvents();
            CurrentScene.Unload();
        }
        CurrentScene = scene;
        SubscribeToEvents();
        CurrentScene.SwitchTo();
        CurrentScene.Load();
    }

    void SubscribeToEvents()
    {
        _events.OnEarlyUpdate += CurrentScene.EarlyUpdate;
        _events.OnUpdate += CurrentScene.Update;
        _events.OnLateUpdate += CurrentScene.LateUpdate;
    }

    void UnsubscribeFromEvents()
    {
        _events.OnEarlyUpdate -= CurrentScene.EarlyUpdate;
        _events.OnUpdate -= CurrentScene.Update;
        _events.OnLateUpdate -= CurrentScene.LateUpdate;
    }
}