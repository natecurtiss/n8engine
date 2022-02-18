using System.Collections.Generic;
using System.Linq;
using N8Engine.Events;

namespace N8Engine;

// TODO: Add Exceptions.
public sealed class GameObject
{
    readonly GameObjectEvents _events;
    readonly List<Component> _components = new();
    bool _isAlive = true;

    internal GameObject(GameObjectEvents events)
    {
        _events = events;
        _events.OnEarlyUpdate.Add(EarlyUpdate);
        _events.OnUpdate.Add(Update);
        _events.OnLateUpdate.Add(LateUpdate);
    }

    public void Destroy()
    {
        _isAlive = false;
        _events.OnEarlyUpdate.Remove(EarlyUpdate);
        _events.OnUpdate.Remove(Update);
        _events.OnLateUpdate.Remove(LateUpdate);
    }

    public T GetComponent<T>() where T : Component => (T) _components.First(component => component.Type == typeof(T));
    
    public void AddComponent<T>(T component) where T : Component
    {
        _components.Add(component);
        component.Type = typeof(T);
        component.Create(this);
    }

    public void RemoveComponent(Component component)
    {
        _components.Remove(component);
        component.Destroy();
    }

    void EarlyUpdate(Frame frame)
    {
        if (!_isAlive)
            return;
        foreach (var component in _components) 
            component.EarlyUpdate(frame);
    }
    
    void Update(Frame frame)
    {
        if (!_isAlive)
            return;
        foreach (var component in _components) 
            component.Update(frame);
    }
    
    void LateUpdate(Frame frame)
    {
        if (!_isAlive)
            return;
        foreach (var component in _components) 
            component.LateUpdate(frame);
    }
}