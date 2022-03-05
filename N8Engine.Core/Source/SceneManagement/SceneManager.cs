using System.Numerics;
using N8Engine.Rendering;

namespace N8Engine.SceneManagement;

public sealed class SceneManager : Module
{
    readonly GameEvents _events;
    Camera _camera;
    
    public Scene CurrentScene { get; private set; }
    
    internal SceneManager(GameEvents events) => _events = events;

    public void Load(Scene scene)
    {
        if (CurrentScene != null)
        {
            UnsubscribeFromEvents();
            CurrentScene.Unload();
        }
        // TODO: Creates a tight coupling so maybe fix this later???
        (_camera ??= Game.Modules.Get<Camera>()).Position = Vector2.Zero;
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