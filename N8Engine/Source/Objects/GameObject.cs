using N8Engine.Core;
using N8Engine.Inputs;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace N8Engine.Objects
{
    /// <summary>
    /// An object that exists and is displayed in a game scene.
    /// </summary>
    public abstract class GameObject
    {
        /// <summary>
        /// The name of the GameObject; serves no purpose other than debugging.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// A vector that holds the position of the GameObject in the scene.
        /// </summary>
        public Vector2 Position { get; set; }
        
        /// <summary>
        /// True when the GameObject is destroyed.
        /// </summary>
        private bool _isDestroyed;

        /// <summary>
        /// Destroys the GameObject.
        /// </summary>
        public void Destroy()
        {
            if (_isDestroyed) return;
            _isDestroyed = true;
        }

        /// <summary>
        /// Initializes the GameObject; used for subscribing to GameLoop and Input events.
        /// </summary>
        internal void Initialize()
        {
            GameLoop.OnUpdate += Update;
            GameLoop.OnRender += Render;
            Input.OnKeyPressed += KeyPressed;
            Input.OnDirectionalInput += DirectionalInput;
            OnStart();
        }

        /// <summary>
        /// Event method called on the first frame.
        /// </summary>
        protected virtual void OnStart() { }
        
        /// <summary>
        /// Subscription method to GameLoop.OnUpdate that exists to call the OnUpdate method that takes in an "in float deltaTime".
        /// </summary>
        /// <param name="deltaTime"> The time since the last frame. </param>
        private void Update(float deltaTime) => OnUpdate(deltaTime);

        /// <summary>
        /// Event method called every frame, before the OnRender method.
        /// </summary>
        /// <param name="deltaTime"> The time since the last frame. </param>
        protected virtual void OnUpdate(in float deltaTime) { }

        /// <summary>
        /// Called every frame after the OnUpdate method, used for rendering GameObjects to the screen.
        /// </summary>
        private void Render()
        {
            Sprite __sprite = Sprite();
            if (__sprite is null) return;
            Window.Render(__sprite, Position);
        }
        
        /// <summary>
        /// Subscription method to Input.OnKeyPressed that exists to call the OnKeyPressed method that takes in an "in Key key".
        /// </summary>
        /// <param name="key"> The key being pressed. </param>
        private void KeyPressed(Key key) => OnKeyPressed(key);

        /// <summary>
        /// Event method called whenever a key is pressed.
        /// </summary>
        /// <param name="key"> The key being pressed. </param>
        protected virtual void OnKeyPressed(in Key key) { }

        /// <summary>
        /// Subscription method to Input.OnDirectionalInput that exists to call the OnDirectionalInput method that takes in an "in Vector2 directionalInput".
        /// </summary>
        /// <param name="directionalInput"> The direction (WASD and arrow keys) of input. </param>
        private void DirectionalInput(Vector2 directionalInput) => OnDirectionalInput(directionalInput);
        
        /// <summary>
        /// Event method called whenever a direction (WASD or arrow keys) is inputted.
        /// </summary>
        /// <param name="directionalInput"> The direction (WASD or arrow keys) being inputted. </param>
        protected virtual void OnDirectionalInput(in Vector2 directionalInput) { }

        /// <summary>
        /// Returns the sprite to render to the screen.
        /// </summary>
        /// <returns> The sprite to render to the screen. </returns>
        protected abstract Sprite Sprite();
    }
}