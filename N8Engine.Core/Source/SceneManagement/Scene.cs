using System;
using System.Collections.Generic;
using N8Engine.Rendering;
using N8Engine.Windowing;

namespace N8Engine.SceneManagement;

public abstract class Scene
{
    public readonly SceneModules Modules = new();
    readonly List<GameObject> _gameObjects = new();
    
    bool _isLoaded;
    bool _isInitialized;
    
    public virtual string Name { get; } = "New Scene";

    protected virtual void Load() { }
    protected virtual void Unload() { }
    
    public GameObject Create(string name) => Create(name, out _);
    
    public GameObject Create(string name, out GameObject gameObject)
    {
        if (!_isLoaded)
        {
            gameObject = null;
            return null;
        }
        gameObject = new(this, name);
        _gameObjects.Add(gameObject);
        return gameObject;
    }

    internal void SwitchTo(Action<SceneModules> onAddModules)
    {
        _isLoaded = true;
        if (!_isInitialized)
        {
            Modules.Initialize(Name);
            _isInitialized = true;
        }
        onAddModules(Modules);
        Modules.OnSceneLoad(this);
        Load();
    }

    internal void SwitchFrom(Action<SceneModules> onRemoveModules)
    {
        _isLoaded = false;
        if (_isInitialized) 
            onRemoveModules(Modules);
        foreach (var gameObject in _gameObjects.ToArray()) 
            gameObject.Destroy();
        _gameObjects.Clear();
        Modules.OnSceneUnload();
        Unload();
    }

    internal void Start()
    {
        foreach (var gameObject in _gameObjects.ToArray()) 
            gameObject.Create();
        foreach (var gameObject in _gameObjects.ToArray()) 
            gameObject.Start();
    }

    internal void Update(Frame frame)
    {
        foreach (var gameObject in _gameObjects.ToArray()) 
            gameObject.EarlyUpdate(frame);
        foreach (var gameObject in _gameObjects.ToArray()) 
            gameObject.Update(frame);
        foreach (var gameObject in _gameObjects.ToArray()) 
            gameObject.LateUpdate(frame);
        Modules.OnSceneUpdate();
    }

    internal void Render()
    {
        foreach (var gameObject in _gameObjects.ToArray()) 
            gameObject.Render();
        Modules.OnSceneRender();
    }

    internal void Destroy(GameObject gameObject) => _gameObjects.Remove(gameObject);

    internal int Count() => _gameObjects.Count;
    internal GameObject First() => _gameObjects[0];
    internal bool Any() => _gameObjects.Count > 0;
}