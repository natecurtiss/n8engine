using System;
using System.Collections.Generic;
using N8Engine.SceneManagement;
using Silk.NET.OpenGL;

namespace N8Engine;

public sealed class GameObject
{
    readonly Dictionary<Type, Component> _components = new();
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
        foreach (var component in _components.Values) 
            component.Destroy();
        _components.Clear();
        _scene.Destroy(this);
    }

    public T GetComponent<T>() where T : Component
    {
        if (IsDestroyed)
            throw new GameObjectIsDestroyedException($"GameObject {Name} is destroyed, you cannot access its components!");
        foreach (var (type, component) in _components)
        {
            if (type == typeof(T))
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
            throw new GameObjectIsDestroyedException($"GameObject {this} is destroyed, you cannot access its components!");
        if (_components.ContainsKey(typeof(T)))
            throw new ComponentAlreadyAttachedException($"GameObject {this} already has a component of type {typeof(T)} attached!");
        _components.Add(typeof(T), component);
        result = component;
        return this;
    }

    public GameObject RemoveComponent<T>() where T : Component
    {
        try
        {
            return RemoveComponent(_components[typeof(T)] as T);
        }
        catch (KeyNotFoundException)
        {
            throw new ComponentNotAttachedException($"Component of type {typeof(T)} is not attached to {this}!");
        }
    }

    public GameObject RemoveComponent<T>(T component) where T : Component
    {
        if (IsDestroyed)
            throw new GameObjectIsDestroyedException($"GameObject {this} is destroyed, you cannot access its components!");
        if (!_components.ContainsKey(typeof(T)))
            throw new ComponentNotAttachedException($"Component of type {typeof(T)} is not attached to {this}!");
        _components.Remove(typeof(T));
        component.Destroy();
        return this;
    }

    internal void EarlyUpdate(Frame frame)
    {
        foreach (var component in _components.Values) 
            component.EarlyUpdate(frame);
    }
    
    internal void Update(Frame frame)
    {
        foreach (var component in _components.Values) 
            component.Update(frame);
    }
    
    internal void LateUpdate(Frame frame)
    {
        foreach (var component in _components.Values) 
            component.LateUpdate(frame);
    }

    internal void Render(GL gl)
    {
        foreach (var component in _components.Values) 
            component.Render(gl);
    }

    public override string ToString() => Name;
}