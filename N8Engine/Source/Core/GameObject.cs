using N8Engine.Inputs;
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
        /// True when the <see cref="GameObject"/> is destroyed.
        /// </summary>
        private bool _isDestroyed;
        
        /// <summary>
        /// The name of the <see cref="GameObject"/> - serves no purpose other than debugging.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// A <see cref="Vector2"/> that holds the position of the <see cref="GameObject"/> in the scene.
        /// </summary>
        public Vector2 Position { get; set; }
        
        /// <summary>
        /// The current <see cref="Sprite"/> of the <see cref="GameObject"/> to render.
        /// </summary>
        public Sprite Sprite { get; set; }
        
        /// <summary>
        /// The sorting order to render the <see cref="GameObject.Sprite"/> against overlapping
        /// <see cref="Sprite">Sprites</see> - a higher value means it will be rendered on top.
        /// </summary>
        public int SortingOrder
        {
            get => Sprite.SortingOrder;
            set => Sprite.SortingOrder = value;
        }

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
        /// Destroys the <see cref="GameObject"/>.
        /// </summary>
        public void Destroy()
        {
            if (_isDestroyed) return;
            _isDestroyed = true;
        }

        /// <summary>
        /// Initializes the <see cref="GameObject"/> - called by <see cref="Create{T}">Create{T}.</see>
        /// </summary>
        private void Initialize()
        {
            GameLoop.OnUpdate += Update;
            GameLoop.OnRender += OnRender;
            Input.OnKeyPressed += KeyPressed;
            Input.OnDirectionalInput += DirectionalInput;
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

        /// <summary>
        /// Subscription to <see cref="Input.OnKeyPressed"/> that exists to call
        /// <see cref="OnKeyPressed">OnKeyPressed.</see>
        /// </summary>
        /// <param name="key"> The <see cref="Key"/> being pressed. </param>
        /// <param name="deltaTime"> The time since the last frame. </param>
        private void KeyPressed(Key key, float deltaTime) => OnKeyPressed(key, deltaTime);

        /// <summary>
        /// Event method called whenever a <see cref="Key"/> is pressed.
        /// </summary>
        /// <param name="key"> The <see cref="Key"/> being pressed. </param>
        /// <param name="deltaTime"> The time since the last frame. </param>
        protected virtual void OnKeyPressed(in Key key, in float deltaTime) { }

        /// <summary>
        /// Subscription to <see cref="Input.OnDirectionalInput"/> that exists to call
        /// <see cref="OnDirectionalInput">OnDirectionalInput.</see>
        /// </summary>
        /// <param name="directionalInput"> The <see cref="Vector2"/> direction (WASD and arrow keys) of input. </param>
        /// <param name="deltaTime"> The time since the last frame. </param>
        private void DirectionalInput(Vector2 directionalInput, float deltaTime) => OnDirectionalInput(directionalInput, deltaTime);
        
        /// <summary>
        /// Event method called whenever a direction (WASD or arrow keys) is inputted.
        /// </summary>
        /// <param name="directionalInput"> The <see cref="Vector2"/> direction (WASD or arrow keys)
        /// being inputted. </param>
        /// <param name="deltaTime"> The time since the last frame. </param>
        protected virtual void OnDirectionalInput(in Vector2 directionalInput, in float deltaTime) { }
    }
}