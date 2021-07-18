using System.Collections.Generic;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace N8Engine.Physics
{
    public sealed class Collider
    {
        // TODO add this to each Scene object later
        private static readonly List<Collider> _allColliders = new();

        internal DebugRectangle DebugRectangle;

        private readonly Transform _transform;
        private Vector _size;
        private Vector _lastPosition = new();
        
        public bool DebugModeEnabled { get; set; }
        public Vector Offset { get; set; }
        public Vector Size
        {
            get => _size;
            set
            {
                if (_size == value) return;
                Rectangle = new Rectangle(value, _transform.Position + Offset);
                DebugRectangle = new DebugRectangle(value, _transform.Position + Offset);
                _size = value;
            }
        }
        
        private Rectangle Rectangle { get; set; }
        private Vector Position => _transform.Position + Offset;

        internal Collider(Transform transform)
        {
            _transform = transform;
            _allColliders.Add(this);
            GameLoop.OnUpdate += Update;
            GameLoop.OnPhysicsUpdate += PhysicsUpdate;
        }

        private void Update(float deltaTime)
        {
            
        }

        private void PhysicsUpdate(float deltaTime)
        {
            foreach (var collider in _allColliders)
            {
                if (collider == this) continue;
                if (!collider.Rectangle.IsOverlapping(Rectangle)) continue;
            }
        }
    }
}