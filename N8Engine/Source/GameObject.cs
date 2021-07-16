using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace N8Engine
{
    /// <summary>
    /// An object that exists and is displayed in a scene.
    /// </summary>
    public abstract class GameObject
    {
        /// <summary>
        /// A <see cref="Vector"/> that holds the position of the <see cref="GameObject"/> in the scene.
        /// </summary>
        public Vector Position { get; set; }

        public SpriteRenderer SpriteRenderer { get; set; } = new();

            /// <summary>
        /// Creates a new <see cref="GameObject"/> of the specified type.
        /// </summary>
        /// <typeparam name="T"> The type of <see cref="GameObject"/> to create. </typeparam>
        /// <returns> The <see cref="GameObject"/> created. </returns>
        public static T Create<T>() where T : GameObject, new()
        {
            T __gameObject = new();
            __gameObject.Initialize();
            return __gameObject;
        }

        /// <summary>
        /// Initializes the <see cref="GameObject"/> - called by <see cref="Create{T}">Create{T}.</see>
        /// </summary>
        private void Initialize()
        {
            GameLoop.OnUpdate += Update;
            GameLoop.OnRender += OnRender;
            OnStart();
        }

        /// <summary>
        /// Event method called on the first frame.
        /// </summary>
        protected virtual void OnStart() { }

        /// <summary>
        /// Subscription to <see cref="GameLoop.OnUpdate"/> that exists to call <see cref="OnUpdate"/>.
        /// </summary>
        /// <param name="deltaTime"> The time since the last frame. </param>
        private void Update(float deltaTime) => OnUpdate(deltaTime);

        /// <summary>
        /// Event method called every frame, before <see cref="OnRender"/>.
        /// </summary>
        /// <param name="deltaTime"> The time since the last frame. </param>
        protected virtual void OnUpdate(in float deltaTime) { }

        /// <summary>
        /// Sends the <see cref="Sprite"/> to the <see cref="Renderer"/> to be rendered -
        /// called every frame after <see cref="OnUpdate">OnUpdate.</see>
        /// </summary>
        private void OnRender() => Renderer.Render(this);
    }
}