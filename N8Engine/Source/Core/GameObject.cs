using System.Collections.Generic;
using System.Linq;
using N8Engine.Events;
using N8Engine.SceneManagement;

namespace N8Engine;

// TODO: Add Exceptions.
public sealed class GameObject
{
    readonly List<Component> _components = new();
    readonly Scene _scene;
    
    public bool IsAlive { get; private set; }
    public string Name { get; set; }

    internal GameObject(Scene scene, string name)
    {
        _scene = scene;
        IsAlive = true;
        Name = name;
    }

    public void Destroy() => IsAlive = false;

    public T GetComponent<T>() where T : Component => (T) _components.First(component => component.Type == typeof(T));
    
    public GameObject AddComponent<T>(T component) where T : Component
    {
        _components.Add(component);
        component.Type = typeof(T);
        component.Create(this, _scene);
        return this;
    }

    public GameObject RemoveComponent(Component component)
    {
        _components.Remove(component);
        component.Destroy();
        return this;
    }

    internal void EarlyUpdate(Frame frame)
    {
        if (!IsAlive)
            return;
        foreach (var component in _components) 
            component.EarlyUpdate(frame);
    }
    
    internal void Update(Frame frame)
    {
        if (!IsAlive)
            return;
        foreach (var component in _components) 
            component.Update(frame);
    }
    
    internal void LateUpdate(Frame frame)
    {
        if (!IsAlive)
            return;
        foreach (var component in _components) 
            component.LateUpdate(frame);
    }
}