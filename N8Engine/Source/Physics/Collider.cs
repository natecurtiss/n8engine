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
        private BoundingBox BoundingBoxCurrentFrame { get; set; }
        private BoundingBox BoundingBoxNextFrame { get; set; }

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
            BoundingBoxCurrentFrame = new BoundingBox(Size, Position);
            BoundingBoxNextFrame = new BoundingBox(Size, Position + Velocity * deltaTime);
        }

        private void OnPhysicsUpdate(float deltaTime)
        {
            foreach (var otherCollider in _allColliders)
            {
                if (otherCollider == this) continue;
                if (BoundingBoxNextFrame.IsOverlapping(otherCollider.BoundingBoxNextFrame))
                {
                    var directionOfCollision = BoundingBoxCurrentFrame.DirectionRelativeTo(otherCollider.BoundingBoxCurrentFrame);
                    Velocity = new Vector
                    (
                        directionOfCollision is Direction.Left or Direction.Right ? 0f : Velocity.X,
                        directionOfCollision is Direction.Top or Direction.Down ? 0f : Velocity.Y
                    );
                    break;
                }
            }
            _transform.Position += Velocity * deltaTime;
            DebugRectangle.Position = Position;
        }
    }
}