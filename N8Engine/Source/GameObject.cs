using N8Engine.Mathematics;
using N8Engine.Physics;
using N8Engine.Rendering;

namespace N8Engine
{
    /// <summary>
    /// An object that exists and is displayed in a scene.
    /// </summary>
    public abstract class GameObject
    {
        public Transform Transform { get; } = new();
        public SpriteRenderer SpriteRenderer { get; } = new();
        public Collider Collider { get; private set; }

        /// <summary>
        /// Creates a new <see cref="GameObject"/> of the specified type.
        /// </summary>
        /// <typeparam name="T"> The type of <see cref="GameObject"/> to create. </typeparam>
        /// <returns> The <see cref="GameObject"/> created. </returns>
        public static T Create<T>() where T : GameObject, new()
        {
            var gameObject = new T();
            gameObject.Initialize();
            return gameObject;
        }

        /// <summary>
        /// Initializes the <see cref="GameObject"/> - called by <see cref="Create{T}">Create{T}.</see>
        /// </summary>
        private void Initialize()
        {
            Collider = new Collider(Transform);
            GameLoop.OnUpdate += OnUpdate;
            GameLoop.OnRender += OnRender;
            OnStart();
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

        /// <summary>
        /// Sends the <see cref="Sprite"/> to the <see cref="Renderer"/> to be rendered -
        /// called every frame after <see cref="OnUpdate">OnUpdate.</see>
        /// </summary>
        private void OnRender()
        {
            Renderer.Render(SpriteRenderer.Sprite, Transform.Position);
            if (Collider.DebugModeEnabled) 
                Renderer.Render(Collider.DebugRectangle.Sprite, Collider.DebugRectangle.Position);
        }
    }
}