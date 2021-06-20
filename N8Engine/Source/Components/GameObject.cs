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
        
        private readonly List<Component> _components = new List<Component>();
        private readonly Component[] _defaultComponents =
        {
            new Transform()
        };

        public GameObject(in string name)
        {
            Name = name;
            AddDefaultComponents();
        }

        private void AddDefaultComponents()
        {
            foreach (Component __defaultComponent in _defaultComponents)
            {
                _components.Add(__defaultComponent);
                __defaultComponent.OnInitialized();
            }
        }

        public GameObject AddComponent<T>() where T : Component, new()
        {
            T __component = new T();
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

        public GameObject RemoveComponent<T>() where T : Component, new()
        {
            T __component = new T();
            
            if (__component is INotRemoveableComponent)
            {
                Exception __exception = new InvalidOperationException($"Component of type {typeof(T)} cannot be removed!");
                InternalExceptions.ThrowException(__exception);
                return this;
            }
            else if (!_components.Contains(__component))
            {
                Exception __exception = new NullReferenceException($"{Name} does not have a component of type {typeof(T)} to remove!")
                InternalExceptions.ThrowException(__exception);
                return this;
            }

            _components.Remove(__component);
            return this;
        }

        public T GetComponent<T>() where T : Component
        {
            foreach (Component __component in _components.Where(component => component.GetType() == typeof(T)))
                return (T) __component;
            return null;
        }
    }
}