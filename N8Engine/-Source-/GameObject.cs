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
        public PhysicsBody PhysicsBody { get; private set; }
        public AnimationPlayer AnimationPlayer { get; private set; }
        
        public static T Create<T>(string name = default) where T : GameObject, new()
        {
            var gameObject = new T();
            SceneManager.CurrentScene.GameObjects.Add(gameObject);
            gameObject.Initialize();
            gameObject.Name = name;
            return gameObject;
        }
        
        public virtual void OnCollidedWith(Collider otherCollider) { }

        public virtual void OnTriggeredBy(Collider otherTrigger) { }
        
        protected virtual void OnStart() { }
        
        protected virtual void OnUpdate(float deltaTime) { }

        public bool Is<T>(out T type) where T : GameObject
        {
            type = this as T;
            return type != null;
        }
        
        public bool Is<T>() where T : GameObject => this is T type;

        public void Destroy()
        {
            GameLoop.OnUpdate -= OnUpdate;
            GameLoop.OnPostUpdate -= OnPostUpdate;
            GameLoop.OnPhysicsUpdate -= OnPhysicsUpdate;
            GameLoop.OnRender -= OnRender;
        }

        private void Initialize()
        {
            Transform = new Transform(this);
            PhysicsBody = new PhysicsBody(this);
            Collider = new Collider(this);
            SpriteRenderer = new SpriteRenderer(this);
            AnimationPlayer = new AnimationPlayer(this);
            GameLoop.OnUpdate += OnUpdate;
            GameLoop.OnPostUpdate += OnPostUpdate;
            GameLoop.OnPhysicsUpdate += OnPhysicsUpdate;
            GameLoop.OnRender += OnRender;
            OnStart();
        }

        private void OnPostUpdate(float deltaTime) => AnimationPlayer.Tick(deltaTime);

        private void OnPhysicsUpdate(float deltaTime)
        {
            PhysicsBody.ApplyGravity();
            Collider.UpdateBoundingBoxes(deltaTime);
            Collider.CheckCollisions();
            PhysicsBody.ApplyVelocity(deltaTime);
        }
        
        private void OnRender()
        {
            if (SpriteRenderer.Sprite != null)
                Renderer.Render(SpriteRenderer.Sprite, Transform.Position, SpriteRenderer.SortingOrder);
            if (Collider.IsDebugModeEnabled)
                Renderer.Render(Collider.DebugMode.Sprite, Collider.DebugMode.Position, (int) Math.Infinity);
        }
    }
}