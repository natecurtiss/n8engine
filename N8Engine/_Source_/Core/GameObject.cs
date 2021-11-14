using System.Collections.Generic;
using N8Engine.Mathematics;

namespace N8Engine
{
    public sealed class GameObject
    {
        public readonly string Name;
        readonly GameObjectEvents _events;
        readonly List<Component> _components = new();
        
        public Vector Position { get; set; }

        public GameObject(string name)
        {
            Name = name;
            _events = Modules.Get<GameObjectEvents>();
            RegisterEvents();
        }
        
        public GameObject(string name, params Component[] components)
        {
            Name = name;
            _events = Modules.Get<GameObjectEvents>();
            RegisterEvents();
            foreach (var component in components)
                Add(component);
        }

        public void Destroy()
        {
            DeregisterEvents();
            foreach (var component in _components)
                Remove(component);
        }

        public void Add<T>(T component) where T : Component
        {
            _components.Add(component);
            component.AttatchTo(this);
        }

        public void Remove<T>(T component) where T : Component
        {
            _components.Remove(component);
            component.Detatch();
        }

        public T Get<T>() where T : Component
        {
            foreach (var component in _components)
                if (component is T t)
                    return t;
            return null;
        }

        void RegisterEvents()
        {
            _events.OnEarlyUpdate += EarlyUpdate;
            _events.OnUpdate += Update;
            _events.OnPhysics += Physics;
            _events.OnLateUpdate += LateUpdate;
            _events.OnRender += Render;
        }

        void DeregisterEvents()
        {
            _events.OnEarlyUpdate -= EarlyUpdate;
            _events.OnUpdate -= Update;
            _events.OnPhysics -= Physics;
            _events.OnLateUpdate -= LateUpdate;
            _events.OnRender -= Render;
        }
        
        void EarlyUpdate(Time time) => _components.ForEach(component => component.EarlyUpdate(time));
        void Update(Time time) => _components.ForEach(component => component.Update(time));
        void Physics(Time time) => _components.ForEach(component => component.Physics(time));
        void LateUpdate(Time time) => _components.ForEach(component => component.LateUpdate(time));
        void Render() => _components.ForEach(component => component.Render());
    }
}