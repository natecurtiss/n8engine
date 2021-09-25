using N8Engine.Animation;
using N8Engine.Mathematics;
using N8Engine.Physics;
using N8Engine.Rendering;
using N8Engine.SceneManagement;

namespace N8Engine
{
    public abstract class GameObject
    {
        private const string DEFAULT_NAME = "new gameobject";
        
        public string Name { get; private set; }
        public Transform Transform { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }
        public Collider Collider { get; private set; }
        public PhysicsBody PhysicsBody { get; private set; }
        public Animator Animator { get; private set; }

        public static T Create<T>(string name = DEFAULT_NAME) where T : GameObject, new()
        {
            var gameObject = new T();
            SceneManager.CurrentScene.Add(gameObject);
            if (name == DEFAULT_NAME) name = $"new {gameObject.GetType()}";
            gameObject.Initialize(name);
            return gameObject;
        }

        public override string ToString() => Name;

        public virtual void OnCollidedWith(Collider otherCollider) { }
        
        public virtual void OnStoppedCollidingWith(Collider otherCollider) { }

        public virtual void OnTriggeredBy(Collider otherTrigger) { }
        
        public virtual void OnStoppedBeingTriggeredBy(Collider otherTrigger) { }
        
        protected virtual void OnStart() { }
        
        protected virtual void OnEarlyUpdate(float deltaTime) { }
        
        protected virtual void OnUpdate(float deltaTime) { }
        
        protected virtual void OnLateUpdate(float deltaTime) { }
        
        protected virtual void OnDestroy() { }

        public bool Is<T>(out T type) where T : IGameObjectInterface
        {
            if (this is not T t)
            {
                type = default;
                return false;
            }
            type = t;
            return type != null;
        }
        
        public bool Is<T>() where T : IGameObjectInterface => this is T;

        public bool IsA<T>(out T type) where T : GameObject
        {
            type = this as T;
            return type != null;
        }
        
        public bool IsA<T>() where T : GameObject => this is T;

        public void Destroy()
        {
            SceneManager.CurrentScene.Remove(this);
            OnDestroy();
            GameLoop.OnEarlyUpdate -= OnEarlyUpdate;
            GameLoop.OnUpdate -= OnUpdate;
            GameLoop.OnPostUpdate -= OnPostUpdate;
            GameLoop.OnPhysicsUpdate -= OnPhysicsUpdate;
            GameLoop.OnLateUpdate -= OnLateUpdate;
            GameLoop.OnRender -= OnRender;
        }

        private void Initialize(string name)
        {
            Name = name;
            Transform = new Transform(this);
            PhysicsBody = new PhysicsBody(this);
            Collider = new Collider(this);
            SpriteRenderer = new SpriteRenderer(gameObject: this);
            Animator = new Animator(this);
            GameLoop.OnEarlyUpdate += OnEarlyUpdate;
            GameLoop.OnUpdate += OnUpdate;
            GameLoop.OnPostUpdate += OnPostUpdate;
            GameLoop.OnPhysicsUpdate += OnPhysicsUpdate;
            GameLoop.OnLateUpdate += OnLateUpdate;
            GameLoop.OnRender += OnRender;
            OnStart();
        }

        private void OnPostUpdate(float deltaTime) => Animator.Tick(deltaTime);

        private void OnPhysicsUpdate(float deltaTime)
        {
            PhysicsBody.ApplyGravity(deltaTime);
            Collider.UpdateBoundingBoxes(deltaTime);
            Collider.CheckCollisions();
            PhysicsBody.ApplyVelocity(deltaTime);
        }

        private void OnRender()
        {
            if (SpriteRenderer.Sprite != null)
                Renderer.Render(SpriteRenderer.SpritePixels, Transform.Position, SpriteRenderer.SortingOrder);
            if ((Collider.IsVisible || PhysicsSettings.ShouldShowAllColliders) && Collider.Size != Vector.Zero)
                Renderer.Render(Collider.Sprite.NotFlipped, Collider.Position, Math.INFINITY - 1);
        }
    }
}