using System.Collections.Generic;
using N8Engine.Utilities;

namespace N8Engine.SceneManagement;

public sealed class SceneModules : ServiceLocator<SceneModule, SceneModuleNotFoundException>
{
    string _sceneName;
    
    internal bool IsInitialized { get; private set; }
    IEnumerable<SceneModule> Modules => Services.Values;
    
    internal SceneModules() { }
    
    protected override SceneModuleNotFoundException ServiceNotFoundException<T>() =>  new($"Module of type {typeof(T)} not found in Scene {_sceneName}!");
    
    internal void Initialize(string sceneName)
    {
        _sceneName = sceneName;
        IsInitialized = true;
    }

    internal void OnSceneLoad(Scene scene)
    {
        foreach (var module in Modules) 
            module.OnSceneLoad(scene);
    }

    internal void OnSceneUpdate()
    {
        foreach (var module in Modules) 
            module.OnSceneUpdate();
    }

    internal void OnSceneRender()
    {
        foreach (var module in Modules) 
            module.OnSceneRender();
    }
}