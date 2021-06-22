using System;
using N8Engine.Core;
using N8Engine.Inputs;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace N8Engine.Objects
{
    public abstract class GameObject
    {
        public string Name { get; set; }
        public Vector2 Position { get; set; }
        
        private bool _isDestroyed;

        public void Destroy()
        {
            if (_isDestroyed) return;
            _isDestroyed = true;
        }

        internal void Initialize()
        {
            GameLoop.OnUpdate += Update;
            GameLoop.OnRender += OnRender;
            Input.OnKeyPressed += KeyPressed;
            Input.OnDirectionalInput += DirectionalInput;
            OnStart();
        }

        protected virtual void OnStart() { }
        
        private void Update(float deltaTime) => OnUpdate(deltaTime);

        protected virtual void OnUpdate(in float deltaTime) { }

        private void OnRender() { }

        private void KeyPressed(Key key) => OnKeyPressed(key);

        protected virtual void OnKeyPressed(in Key key) { }

        private void DirectionalInput(Vector2 directionInput) => OnDirectionalInput(directionInput);
        
        protected virtual void OnDirectionalInput(in Vector2 directionalInput) { }

        protected abstract Sprite RenderSprite();
    }
}