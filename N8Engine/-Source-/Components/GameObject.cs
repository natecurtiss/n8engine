using System.Collections.Generic;
using N8Engine.Rendering;
using static N8Engine.InternalServices;

namespace N8Engine
{
    public sealed class GameObject
    {
        readonly List<Component> _components = new();
        public Transform Transform { get; private set; }

        public GameObject()
        {
            UpdateEvents.OnUpdate += OnUpdate;
            UpdateEvents.OnPhysicsUpdate += OnPhysicsUpdate;
            UpdateEvents.OnLateUpdate += OnLateUpdate;
            RenderingEvents.OnPreRender += OnPreRender;
            AddComponent<Transform>(out var transform);
            Transform = transform;
        }

        public void Destroy()
        {
            UpdateEvents.OnUpdate -= OnUpdate;
            UpdateEvents.OnPhysicsUpdate -= OnPhysicsUpdate;
            UpdateEvents.OnLateUpdate -= OnLateUpdate;
            RenderingEvents.OnPreRender -= OnPreRender;
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

        void OnUpdate(float deltaTime)
        {
            foreach (var component in _components)
                component.Update(deltaTime);
        }

        void OnPhysicsUpdate(float deltaTime)
        {
            foreach (var component in _components)
                component.PhysicsUpdate(deltaTime);
        }
        
        void OnLateUpdate(float deltaTime)
        {
            foreach (var component in _components)
                component.LateUpdate(deltaTime);
        }

        void OnPreRender(IRenderer renderer)
        {
            foreach (var component in _components)
                component.RenderUpdate(renderer);
        }
    }
}