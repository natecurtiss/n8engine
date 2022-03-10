using System.Collections.Generic;

namespace N8Engine.SceneManagement;

public abstract class Scene
{
    public readonly SceneModules Modules = new();
    readonly List<GameObject> _gameObjects = new();
    bool _isLoaded;

    public virtual string Name { get; } = "New Scene";

    protected abstract void Load();
    
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

    internal void SwitchTo()
    {
        if (!Modules.IsInitialized)
            Modules.Initialize(Name);
        _isLoaded = true;
        Modules.OnSceneLoad(this);
        Load();
    }

    internal void Unload()
    {
        _isLoaded = false;
        foreach (var gameObject in _gameObjects) 
            gameObject.Destroy();
        _gameObjects.Clear();
    }

    internal void Start()
    {
        foreach (var gameObject in _gameObjects) 
            gameObject.Start();
    }

    internal void Update(Frame frame)
    {
        foreach (var gameObject in _gameObjects) 
            gameObject.EarlyUpdate(frame);
        foreach (var gameObject in _gameObjects) 
            gameObject.Update(frame);
        foreach (var gameObject in _gameObjects) 
            gameObject.LateUpdate(frame);
    }

    internal void Render()
    {
        foreach (var gameObject in _gameObjects) 
            gameObject.Render();
    }

    internal void Destroy(GameObject gameObject) => _gameObjects.Remove(gameObject);

    internal int Count() => _gameObjects.Count;
    internal GameObject First() => _gameObjects[0];
    internal bool Any() => _gameObjects.Count > 0;
}