using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using N8Engine.Exceptions;

namespace N8Engine.Components
{
    [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
    public sealed class GameObject
    {
        public string Name { get; set; }
        
        private readonly List<Component> _components = new List<Component>()
        {
            new Transform()
        };

        public GameObject(in string name)
        {
            Name = name;
            foreach (Component __component in _components)
                __component.OnInitialized();
        }
        
        public T GetComponent<T>() where T : Component
        {
            foreach (Component __component in _components.Where(component => component.GetType() == typeof(T)))
                return (T) __component;
            return null;
        }

        public GameObject AddComponent<T>() where T : Component, new()
        {
            AddComponent<T>(out T __component);
            return this;
        }
        
        public GameObject AddComponent<T>(out T component) where T : Component, new()
        {
            T __component = new T();
            component = __component;
            if (__component is INotAddableComponent)
            {
                Exception __exception = new InvalidOperationException($"Component of type {typeof(T)} cannot be added!");
                InternalExceptions.ThrowException(__exception);
                return this;
            }
            _components.Add(__component);
            __component.OnInitialized();
            return this;
        }
    }
}