using System;
using System.Collections.Generic;
using N8Engine.Utilities;

namespace N8Engine.SceneManagement;

public abstract class Scene : ServiceLocator<Element, ElementNotFoundException>, Elements
{
    readonly List<GameObject> _gameObjects = new();
    bool _isLoaded;

    public virtual string Name { get; } = "New Scene";
    IEnumerable<Element> Cogs => Services.Values;

    public T Get<T>() where T : Element => Find<T>();
    void Elements.Add<T>(T cog) => Register(cog);
    void Elements.Remove<T>() => Deregister<T>();

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

    internal void SwitchTo(Action<Elements> onAddCogs)
    {
        _isLoaded = true;
        onAddCogs(this);
        foreach (var cog in Cogs)
            cog.OnSceneLoad(this);
        Load();
    }

    internal void SwitchFrom(Action<Elements> onRemoveCogs)
    {
        _isLoaded = false;
        foreach (var gameObject in _gameObjects.ToArray()) 
            gameObject.Destroy();
        _gameObjects.Clear();
        onRemoveCogs(this);
        foreach (var cog in Cogs)
            cog.OnSceneUnload();
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
        foreach (var cog in Cogs)
            cog.OnSceneUpdate();
    }

    internal void Render()
    {
        foreach (var gameObject in _gameObjects.ToArray()) 
            gameObject.Render();
        foreach (var cog in Cogs)
            cog.OnSceneRender();
    }

    internal void Destroy(GameObject gameObject) => _gameObjects.Remove(gameObject);

    internal new int Count() => _gameObjects.Count;
    internal GameObject First() => _gameObjects[0];
    internal bool Any() => _gameObjects.Count > 0;
}