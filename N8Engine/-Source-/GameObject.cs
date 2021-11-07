using System.Collections.Generic;
using N8Engine.Rendering;

namespace N8Engine
{
    public sealed class GameObject
    {
        public readonly string Name;
        readonly List<Component> _components = new();
        
        public Transform Transform { get; private set; }

        GameObject(string name)
        {
            Name = name;
            AddComponent<Transform>(out var transform);
            Services.SceneManager.AddToCurrentScene(this);
            Transform = transform;
        }

        public static GameObject Create(string name = "some gameObject you didn't name lol")
        {
            var gameObject = new GameObject(name);
            return gameObject;
        }

        public void Destroy()
        {
            Services.SceneManager.RemoveFromCurrentScene(this);
            foreach (var component in _components)
                component.Destroy();
        }
        
        public GameObject AddComponent<T>(out T component) where T : Component, new()
        {
            (component = new T()).Give(this);
            if (!component.CanHaveMultiple)
                if (HasComponent<T>())
                    throw new CannotHaveMultipleComponentException($"You aren't allowed to add a component of type {component.GetType()}.");
            _components.Add(component);
            return this;
        }

        public GameObject AddComponent<T>() where T : Component, new() => AddComponent<T>(out var _);
        
        public GameObject RemoveComponent(Component component)
        {
            if (!_components.Contains(component))
                throw new ComponentIsNotFoundException($"{this} does not have the specified {component} attached to remove.");
            _components.Remove(component);
            return this;
        }

        public bool HasComponent<T>() where T : Component => GetComponent<T>() != null;

        public T GetComponent<T>() where T : Component
        {
            foreach (var component in _components)
                if (component is T t)
                    return t;
            return null;
        }

        internal void OnUpdate(float deltaTime)
        {
            foreach (var component in _components)
                component.Update(deltaTime);
        }

        internal void OnPhysicsUpdate(float deltaTime)
        {
            foreach (var component in _components)
                component.PhysicsUpdate(deltaTime);
        }
        
        internal void OnLateUpdate(float deltaTime)
        {
            foreach (var component in _components)
                component.LateUpdate(deltaTime);
        }

        internal void OnPreRender(IRenderer renderer)
        {
            foreach (var component in _components)
                component.Render(renderer);
        }
    }
}