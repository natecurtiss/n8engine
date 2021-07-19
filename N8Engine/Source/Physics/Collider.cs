using System.Collections.Generic;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace N8Engine.Physics
{
    public sealed class Collider : IMoveable
    {
        // TODO add this to each Scene object later
        private static readonly List<Collider> _allColliders = new();
        
        internal readonly DebugRectangle DebugRectangle;

        private readonly Transform _transform;
        private Vector _size;
        private Vector _lastPosition;
        private Vector _positionAfterCollision;
        
        public bool DebugModeEnabled { get; set; }
        public Vector Offset { get; set; }
        public Vector Size
        {
            get => _size;
            set
            {
                if (_size == value) return;
                DebugRectangle.Size = value;
                Rectangle.Size = value;
                _size = value;
            }
        }
        public Vector Position => _transform.Position + Offset;
        

        private Rectangle Rectangle { get; }
        private Vector Velocity { get; set; }

        internal Collider(Transform transform)
        {
            _transform = transform;
            _lastPosition = _transform.Position;
            _allColliders.Add(this);
            Rectangle = new Rectangle(Size, this);
            DebugRectangle = new DebugRectangle(Size, this);
            GameLoop.OnUpdate += Update;
            GameLoop.OnPrePhysicsUpdate += PrePhysicsUpdate;
            GameLoop.OnPostPhysicsUpdate += PostPhysicsUpdate;
        }

        private void Update(float deltaTime)
        {
            Velocity = (_transform.Position - _lastPosition) * deltaTime;
            _lastPosition = _transform.Position;
        }

        private void PrePhysicsUpdate(float deltaTime)
        {
            _positionAfterCollision = Position;
            if (Velocity == Vector.Zero) return;
            if (Size == Vector.Zero) return;
            foreach (var otherCollider in _allColliders)
            {
                if (otherCollider == this) continue;
                if (!otherCollider.Rectangle.IsOverlapping(Rectangle)) continue;
                if (otherCollider.Size == Vector.Zero) continue;

                var x = Velocity.X > 0 ? otherCollider.Rectangle.Left.X - Rectangle.Extents.X : otherCollider.Rectangle.Right.X + Rectangle.Extents.X;
                var y = Velocity.Y > 0 ? otherCollider.Rectangle.Bottom.Y - Rectangle.Extents.Y : otherCollider.Rectangle.Top.Y + Rectangle.Extents.Y;
                _positionAfterCollision = new Vector(x, y);
                break;
            }
        }

        private void PostPhysicsUpdate(float deltaTime)
        {
            _transform.Position = _positionAfterCollision - Offset;
        }
    }
}