using System.Collections.Generic;
using N8Engine.SceneManagement;

namespace N8Engine;

// ReSharper disable ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
public sealed class GameObject
{
    readonly List<Component> _components = new();
    readonly Scene _scene;
    
    public bool IsDestroyed { get; private set; }
    public string Name { get; set; }

    internal GameObject(Scene scene, string name)
    {
        _scene = scene;
        Name = name;
    }

    public void Destroy()
    {
        IsDestroyed = true;
        foreach (var component in _components) 
            component.Destroy();
        _components.Clear();
        _scene.Destroy(this);
    }

    public T GetComponent<T>() where T : Component
    {
        if (IsDestroyed)
            throw new GameObjectIsDestroyedException($"GameObject {Name} is destroyed, you cannot access its components!");
        foreach (var component in _components)
        {
            if (component.Type == typeof(T))
            {
                var component1 = component as T;
                return component1;
            }
        }
        return null;
    }
    
    public GameObject AddComponent<T>() where T : Component, new() => AddComponent(new T());
    public GameObject AddComponent<T>(out T result) where T : Component, new() => AddComponent(new(), out result);
    public GameObject AddComponent<T>(T component) where T : Component => AddComponent(component, out _);
    public GameObject AddComponent<T>(T component, out T result) where T : Component
    {
        if (IsDestroyed)
            throw new GameObjectIsDestroyedException($"GameObject {Name} is destroyed, you cannot access its components!");
        _components.Add(component);
        component.Type = typeof(T);
        component.Create(this, _scene);
        result = component;
        return this;
    }

    public GameObject RemoveComponent(Component component)
    {
        if (IsDestroyed)
            throw new GameObjectIsDestroyedException($"GameObject {this} is destroyed, you cannot access its components!");
        if (!_components.Contains(component))
            throw new MissingComponentException($"Component of type {component.Type} is not attached to {this}!");
        _components.Remove(component);
        component.Destroy();
        return this;
    }

    internal void EarlyUpdate(Frame frame)
    {
        foreach (var component in _components) 
            component.EarlyUpdate(frame);
    }
    
    internal void Update(Frame frame)
    {
        foreach (var component in _components) 
            component.Update(frame);
    }
    
    internal void LateUpdate(Frame frame)
    {
        foreach (var component in _components) 
            component.LateUpdate(frame);
    }

    public override string ToString() => Name;
}