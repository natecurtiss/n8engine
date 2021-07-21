using System.Collections.Generic;
using N8Engine.Mathematics;

namespace N8Engine.Physics
{
    public sealed class Collider
    {
        // TODO add this to each Scene object later
        private static readonly List<Collider> _allColliders = new();
        internal readonly DebugRectangle DebugRectangle;
        private readonly Transform _transform;
        private Vector _size;

        public Vector Velocity { get; set; }
        public bool IsDebugModeEnabled { get; set; }
        public Vector Offset { get; set; }
        public Vector Size
        {
            get => _size;
            set
            {
                if (_size == value) return;
                DebugRectangle.Size = value;
                _size = value;
            }
        }
        public Vector Position => _transform.Position + Offset;
        private Rectangle BoundingBox { get; set; }

        internal Collider(Transform transform)
        {
            _transform = transform;
            _allColliders.Add(this);
            DebugRectangle = new DebugRectangle(Size, Position);
            GameLoop.OnLateUpdate += OnLateUpdate;
            GameLoop.OnPhysicsUpdate += OnPhysicsUpdate;
        }

        private void OnLateUpdate(float deltaTime)
        {
            DebugRectangle.Position = Position;
            BoundingBox = new Rectangle(Size, Position + Velocity * deltaTime);
        }

        private void OnPhysicsUpdate(float deltaTime)
        {
            foreach (var otherCollider in _allColliders)
            {
                if (otherCollider == this) continue;
                if (BoundingBox.IsOverlapping(otherCollider.BoundingBox))
                {
                    Velocity = Vector.Zero;
                    break;
                }
            }
            _transform.Position += Velocity * deltaTime;
        }
    }
}