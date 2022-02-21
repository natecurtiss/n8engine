using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace N8Engine.SceneManagement;

// TODO: Add Exceptions (maybe).
public abstract class Scene : IEnumerable<GameObject>
{
    bool _isLoaded;
    
    readonly List<GameObject> _gameObjects = new();

    public abstract void Load();

    internal void SwitchTo() => _isLoaded = true;

    internal void Unload()
    {
        _isLoaded = false;
        foreach (var gameObject in this.ToArray()) 
            gameObject.Destroy();
        _gameObjects.Clear();
    }

    internal void EarlyUpdate(Frame frame)
    {
        foreach (var gameObject in this.ToArray()) 
            gameObject.EarlyUpdate(frame);
    }
    
    internal void Update(Frame frame)
    {
        foreach (var gameObject in this.ToArray()) 
            gameObject.Update(frame);
    }
    
    internal void LateUpdate(Frame frame)
    {
        foreach (var gameObject in this.ToArray()) 
            gameObject.LateUpdate(frame);
    }

    internal void Destroy(GameObject gameObject) => _gameObjects.Remove(gameObject);
    
    public GameObject Create(string name = "some gameobject without a name")
    {
        // TODO: Maybe throw an exception.
        if (!_isLoaded)
            return null;
        var gameObject = new GameObject(this, name);
        _gameObjects.Add(gameObject);
        return gameObject;
    }

    public IEnumerator<GameObject> GetEnumerator() => _gameObjects.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _gameObjects.GetEnumerator();
}