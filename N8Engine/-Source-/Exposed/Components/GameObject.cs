using System.Collections.Generic;

namespace N8Engine
{
    public sealed class GameObject
    {
        readonly List<Component> _components = new();
        public Transform Transform { get; private set; }

        public GameObject()
        {
            AddComponent<Transform>(out var transform);
            Transform = transform;
        }

        public void Destroy()
        {
            
        }

        public GameObject ChangeTransform<T>() where T : Transform, new()
        {
            RemoveComponent(Transform);
            AddComponent<T>(out var transform);
            Transform = transform;
            return this;
        }
        
        public GameObject AddComponent<T>(out T component) where T : Component, new()
        {
            (component = new T()).Give(this);
            if (!component.CanBeAddedByUser)
                throw new NotAddableComponentException($"You aren't allowed to add a component of type {component.GetType()}.");
            _components.Add(component);
            return this;
        }
        
        public GameObject RemoveComponent(Component component)
        {
            if (!_components.Contains(component))
                throw new NotAddableComponentException($"{this} does not have the specified {component} attached to remove.");
            _components.Remove(component);
            return this;
        }
        
        // TODO: GetComponent<T>().d
    }
}