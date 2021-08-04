using N8Engine.Mathematics;
using N8Engine.Physics;
using N8Engine.Rendering;
using N8Engine.Rendering.Animation;
using N8Engine.SceneManagement;

namespace N8Engine
{
    /// <summary>
    /// An object that exists and is displayed in a scene.
    /// </summary>
    public abstract class GameObject
    {
        public string Name { get; set; }
        public Transform Transform { get; } = new();
        public SpriteRenderer SpriteRenderer { get; } = new();
        public Collider Collider { get; private set; }
        public AnimationPlayer AnimationPlayer { get; private set; }

        /// <summary>
        /// Creates a new <see cref="GameObject"/> of the specified type.
        /// </summary>
        /// <typeparam name="T"> The type of <see cref="GameObject"/> to create. </typeparam>
        /// <returns> The <see cref="GameObject"/> created. </returns>
        public static T Create<T>(string name = default) where T : GameObject, new()
        {
            var gameObject = new T();
            SceneManager.CurrentScene.GameObjects.Add(gameObject);
            gameObject.Initialize();
            gameObject.Name = name;
            return gameObject;
        }
        
        /// <summary>
        /// Event method called on the first frame.
        /// </summary>
        protected virtual void OnStart() { }

        /// <summary>
        /// Event method called every frame, before <see cref="OnRender"/>.
        /// </summary>
        /// <param name="deltaTime"> The time since the last frame. </param>
        protected virtual void OnUpdate(float deltaTime) { }
        
        protected virtual void OnCollision(Collider otherCollider) { }

        public void Destroy()
        {
            Collider.Destroy();
            AnimationPlayer.Destroy();
            GameLoop.OnUpdate -= OnUpdate;
            GameLoop.OnRender -= OnRender;
        }

        internal void CollidedWith(Collider otherCollider) => OnCollision(otherCollider);

        /// <summary>
        /// Initializes the <see cref="GameObject"/> - called by <see cref="Create{T}">Create{T}.</see>
        /// </summary>
        private void Initialize()
        {
            Collider = new Collider(this);
            AnimationPlayer = new AnimationPlayer(this);
            GameLoop.OnUpdate += OnUpdate;
            GameLoop.OnRender += OnRender;
            OnStart();
        }

        /// <summary>
        /// Sends the <see cref="Sprite"/> to the <see cref="Renderer"/> to be rendered -
        /// called every frame after <see cref="OnUpdate">OnUpdate.</see>
        /// </summary>
        private void OnRender()
        {
            if (SpriteRenderer.Sprite != null)
                Renderer.Render(SpriteRenderer.Sprite, Transform.Position);
            if (Collider.IsDebugModeEnabled) 
                Renderer.Render(Collider.DebugRectangle.Sprite, Collider.DebugRectangle.Position);
        }
    }
}