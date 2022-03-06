using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using N8Engine.Rendering;
using N8Engine.Windowing;

namespace N8Engine.SceneManagement;

public abstract class Scene : IEnumerable<GameObject>
{
    readonly List<GameObject> _gameObjects = new();
    bool _isLoaded;
    
    public Camera Camera { get; private set; }
    internal readonly Renderer Renderer = new();

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

    internal void SwitchTo(WindowSize windowSize)
    {
        Camera = new(Vector2.Zero, 1f, windowSize);
        _isLoaded = true;
    }

    internal void Unload()
    {
        _isLoaded = false;
        foreach (var gameObject in this.ToArray()) 
            gameObject.Destroy();
        _gameObjects.Clear();
    }

    internal void Initialize() => Renderer.Initialize();

    internal void Update(Frame frame)
    {
        foreach (var gameObject in this.ToArray()) 
            gameObject.EarlyUpdate(frame);
        foreach (var gameObject in this.ToArray()) 
            gameObject.Update(frame);
        foreach (var gameObject in this.ToArray()) 
            gameObject.LateUpdate(frame);
    }

    internal void Render()
    {
        foreach (var gameObject in this.ToArray()) 
            gameObject.Render();
    }

    internal void Destroy(GameObject gameObject) => _gameObjects.Remove(gameObject);

    public IEnumerator<GameObject> GetEnumerator() => _gameObjects.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _gameObjects.GetEnumerator();
}