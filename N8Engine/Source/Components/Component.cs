using N8Engine.Core;

namespace N8Engine.Components
{
    public abstract class Component : Object
    {
        public static implicit operator GameObject(in Component component) => component.GameObject;
        
        public GameObject GameObject { get; private set; }

        public void Initialize(in GameObject gameObject) => GameObject = gameObject;

        protected virtual void OnUpdate(in float deltaTime)
        {
            
        }
    }
}