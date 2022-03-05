using System.Numerics;
using N8Engine.Rendering;

namespace N8Engine.SceneManagement;

public sealed class SceneManager : Module
{
    readonly Loop _loop;
    Camera _camera;
    
    public Scene CurrentScene { get; private set; }
    
    internal SceneManager(Loop loop) => _loop = loop;

    public void Load(Scene scene)
    {
        if (CurrentScene != null)
        {
            _loop.OnUpdate -= CurrentScene.Update;
            _loop.OnRender -= CurrentScene.Render;
            CurrentScene.Unload();
        }
        // TODO: Creates a tight coupling so maybe fix this later???
        (_camera ??= Game.Modules.Get<Camera>()).Position = Vector2.Zero;
        CurrentScene = scene;
        _loop.OnUpdate += CurrentScene.Update;
        _loop.OnRender += CurrentScene.Render;
        CurrentScene.SwitchTo();
        CurrentScene.Load();
    }
}