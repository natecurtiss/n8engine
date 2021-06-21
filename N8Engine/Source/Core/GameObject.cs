using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using N8Engine.Components;
using N8Engine.Exceptions;

namespace N8Engine.Core
{
    [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
    public sealed class GameObject : Object
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
                __component.Initialize(this);
        }

        public InternalGameObject<T> GetComponent<T>() where T : Component
        {
            foreach (Component __component in _components.Where(component => component.GetType() == typeof(T)))
                return new InternalGameObject<T>(__component as T);
            return new InternalGameObject<T>(null);
        }

        public InternalGameObject<T> AddComponent<T>() where T : Component, new()
        {
            T __component = new T();
            if (__component is INotAddableComponent)
            {
                Exception __exception = new InvalidOperationException($"Component of type {typeof(T)} cannot be added!");
                InternalExceptions.ThrowException(__exception);
                return new InternalGameObject<T>(null);
            }

            _components.Add(__component);
            __component.Initialize(this);
            return new InternalGameObject<T>(__component);
        }
        
        internal void RemoveComponent(in Component component) =>
            _components.Remove(component);

        internal void DestroyAllComponents()
        {
            List<Component> __components = _components.ToList();
            __components.ForEach(Destroy);
        }
    }
}