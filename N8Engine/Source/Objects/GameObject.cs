using N8Engine.Core;
using N8Engine.Inputs;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace N8Engine.Objects
{
    /// <summary>
    /// An object that exists and is displayed in a scene.
    /// </summary>
    public abstract class GameObject
    {
        /// <summary>
        /// The name of the <see cref="GameObject"/>; serves no purpose other than debugging.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// A <see cref="Vector2"/> that holds the position of the <see cref="GameObject"/> in the scene.
        /// </summary>
        public Vector2 Position { get; set; }
        
        /// <summary>
        /// True when the <see cref="GameObject"/> is destroyed.
        /// </summary>
        private bool _isDestroyed;

        /// <summary>
        /// Destroys the <see cref="GameObject"/>.
        /// </summary>
        public void Destroy()
        {
            if (_isDestroyed) return;
            _isDestroyed = true;
        }

        /// <summary>
        /// Initializes the <see cref="GameObject"/>; used for subscribing to <see cref="GameLoop"/> and <see cref="Input"/> events.
        /// </summary>
        internal void Initialize()
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
        /// Called every frame after <see cref="OnUpdate"/>, used for rendering <see cref="GameObject"/>s to the <see cref="Window"/>.
        /// </summary>
        private void OnRender()
        {
            Sprite __sprite = Sprite();
            if (__sprite is null) return;
            Window.AddToRenderQueue(__sprite, Position);
        }
        
        /// <summary>
        /// Subscription to <see cref="Input.OnKeyPressed"/> that exists to call <see cref="OnKeyPressed"/>.
        /// </summary>
        /// <param name="key"> The <see cref="Key"/> being pressed. </param>
        private void KeyPressed(Key key) => OnKeyPressed(key);

        /// <summary>
        /// Event method called whenever a <see cref="Key"/> is pressed.
        /// </summary>
        /// <param name="key"> The <see cref="Key"/> being pressed. </param>
        protected virtual void OnKeyPressed(in Key key) { }

        /// <summary>
        /// Subscription to <see cref="Input.OnDirectionalInput"/> that exists to call <see cref="OnDirectionalInput"/>.
        /// </summary>
        /// <param name="directionalInput"> The <see cref="Vector2"/> direction (WASD and arrow keys) of input. </param>
        private void DirectionalInput(Vector2 directionalInput) => OnDirectionalInput(directionalInput);
        
        /// <summary>
        /// Event method called whenever a direction (WASD or arrow keys) is inputted.
        /// </summary>
        /// <param name="directionalInput"> The <see cref="Vector2"/> direction (WASD or arrow keys) being inputted. </param>
        protected virtual void OnDirectionalInput(in Vector2 directionalInput) { }

        /// <summary>
        /// Returns the <see cref="N8Engine.Rendering.Sprite"/> to render to the <see cref="Window"/>.
        /// </summary>
        /// <returns> The <see cref="N8Engine.Rendering.Sprite"/> to render to the <see cref="Window"/>. </returns>
        protected abstract Sprite Sprite();
    }
}