using System.Collections.Generic;
using N8Engine.Windowing;

namespace N8Engine.SceneManagement;

// TODO: Scene module system.
public abstract class Scene
{
    readonly List<GameObject> _gameObjects = new();
    bool _isLoaded;

    public abstract void Load();
    
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

    // TODO: Get rid of this.
    internal void SwitchTo(WindowSize windowSize) => _isLoaded = true;

    internal void Unload()
    {
        _isLoaded = false;
        foreach (var gameObject in _gameObjects.ToArray()) 
            gameObject.Destroy();
        _gameObjects.Clear();
    }
    
    internal void Start() { }

    internal void Update(Frame frame)
    {
        foreach (var gameObject in _gameObjects.ToArray()) 
            gameObject.EarlyUpdate(frame);
        foreach (var gameObject in _gameObjects.ToArray()) 
            gameObject.Update(frame);
        foreach (var gameObject in _gameObjects.ToArray()) 
            gameObject.LateUpdate(frame);
    }

    internal void Render()
    {
        foreach (var gameObject in _gameObjects.ToArray()) 
            gameObject.Render();
    }

    internal void Destroy(GameObject gameObject) => _gameObjects.Remove(gameObject);

    internal int Count() => _gameObjects.Count;
    internal GameObject First() => _gameObjects[0];
    internal bool Any() => _gameObjects.Count > 0;
}