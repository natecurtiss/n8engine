using N8Engine.Mathematics;
using N8Engine.Physics;
using N8Engine.Rendering;
using N8Engine.Rendering.Animation;
using N8Engine.SceneManagement;

namespace N8Engine
{
    public abstract class GameObject
    {
        public string Name { get; set; }
        public Transform Transform { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }
        public Collider Collider { get; private set; }
        public AnimationPlayer AnimationPlayer { get; private set; }
        
        public static T Create<T>(string name = default) where T : GameObject, new()
        {
            var gameObject = new T();
            SceneManager.CurrentScene.GameObjects.Add(gameObject);
            gameObject.Initialize();
            gameObject.Name = name;
            return gameObject;
        }
        
        protected virtual void OnStart() { }
        
        protected virtual void OnUpdate(float deltaTime) { }
        
        protected virtual void OnCollision(Collider otherCollider) { }
        
        protected virtual void OnTrigger(Collider otherTrigger) { }

        public void Destroy()
        {
            Collider.Destroy();
            AnimationPlayer.Destroy();
            GameLoop.OnUpdate -= OnUpdate;
            GameLoop.OnRender -= OnRender;
        }

        internal void CollidedWith(Collider otherCollider) => OnCollision(otherCollider);

        internal void TriggeredWith(Collider otherTrigger) => OnTrigger(otherTrigger);
        
        private void Initialize()
        {
            Transform = new Transform();
            SpriteRenderer = new SpriteRenderer();
            Collider = new Collider(this);
            AnimationPlayer = new AnimationPlayer(this);
            GameLoop.OnUpdate += OnUpdate;
            GameLoop.OnRender += OnRender;
            OnStart();
        }
        
        private void OnRender()
        {
            if (SpriteRenderer.Sprite != null)
                Renderer.Render(SpriteRenderer.Sprite, Transform.Position);
            if (Collider.IsDebugModeEnabled) 
                Renderer.Render(Collider.Debug.Sprite, Collider.Debug.Position);
        }
    }
}