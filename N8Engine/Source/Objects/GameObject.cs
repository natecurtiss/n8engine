using N8Engine.Core;
using N8Engine.Rendering;

namespace N8Engine.Objects
{
    public abstract class GameObject
    {
        public string Name { get; set; }
        public bool IsDestroyed { get; private set; }

        internal void Initialize()
        {
            Application.OnUpdate += Update;
            Application.OnRender += OnRender;
            OnStart();
        }

        protected virtual void OnStart() { }
        
        private void Update(float deltaTime) => OnUpdate(deltaTime);

        protected virtual void OnUpdate(in float deltaTime) { }

        private void OnRender()
        {
            
        }

        protected abstract Sprite RenderSprite();
        
        public void Destroy()
        {
            if (IsDestroyed) return;
            IsDestroyed = true;
        }
    }
}