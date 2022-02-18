using System.Collections;
using System.Collections.Generic;

namespace N8Engine.SceneManagement;

public abstract class Scene : IEnumerable<GameObject>
{
    readonly List<GameObject> _gameObjects = new();

    public abstract void Load();

    internal void Unload()
    {
        foreach (var gameObject in this) 
            gameObject.Destroy();
        _gameObjects.Clear();
    }

    internal void EarlyUpdate(Frame frame)
    {
        foreach (var gameObject in this) 
            gameObject.EarlyUpdate(frame);
    }
    
    internal void Update(Frame frame)
    {
        foreach (var gameObject in this) 
            gameObject.Update(frame);
    }
    
    internal void LateUpdate(Frame frame)
    {
        foreach (var gameObject in this) 
            gameObject.LateUpdate(frame);
    }

    protected GameObject Create(string name = "some gameobject without a name")
    {
        var gameObject = new GameObject(this, name);
        _gameObjects.Add(gameObject);
        return gameObject;
    }

    public IEnumerator<GameObject> GetEnumerator() => _gameObjects.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _gameObjects.GetEnumerator();
}