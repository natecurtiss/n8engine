using System.Collections.Generic;
using System.Linq;
using N8Engine.Mathematics;
using N8Engine.Rendering;
using N8Engine.SceneManagement;

namespace N8Engine.Physics
{
    public sealed class Collider : Component
    {
        internal readonly DebugCollider DebugMode;
        readonly List<Collider> _collidersCollidingWithThisFrame = new();
        readonly List<Collider> _collidersCollidingWithLastFrame = new();
        readonly PhysicsBody _physicsBody;
        Vector _size;

        public Transform Transform => GameObject.Transform;
        public IEnumerable<Collider> Contacts => _collidersCollidingWithThisFrame;
        public bool ShowDebugCollider { get; set; }
        public bool IsTrigger { get; set; }
        public Vector Offset { get; set; }
        public Vector Size
        {
            get => _size;
            set
            {
                if (_size == value) return;
                DebugMode.Size = value;
                _size = value;
            }
        }
        internal Vector Position => Transform.Position + Offset;
        
        Vector ActualSize => new(Size.X * Window.RATIO_OF_HORIZONTAL_PIXELS_TO_VERTICAL_PIXELS, Size.Y);
        BoundingBox BoundingBoxCurrentFrame { get; set; }
        BoundingBox BoundingBoxNextFrame { get; set; }

        internal Collider(GameObject gameObject) : base(gameObject)
        {
            DebugMode = new DebugCollider(this);
            _physicsBody = gameObject.PhysicsBody;
        }

        internal void UpdateBoundingBoxes(float deltaTime)
        {
            BoundingBoxCurrentFrame = new BoundingBox(ActualSize, Position);
            BoundingBoxNextFrame = new BoundingBox(ActualSize, Position + _physicsBody.Velocity * deltaTime);
        }

        internal void CheckCollisions()
        {
            _collidersCollidingWithLastFrame.Clear();
            foreach (var collider in _collidersCollidingWithThisFrame)
                _collidersCollidingWithLastFrame.Add(collider);
            _collidersCollidingWithThisFrame.Clear();

            if (Size == Vector.Zero) return;
            
            foreach (var otherGameObject in SceneManager.CurrentScene.GameObjects)
            {
                var otherCollider = otherGameObject.Collider;
                if (otherCollider == this) continue;
                if (otherCollider.Size == Vector.Zero) continue;
                
                if (BoundingBoxNextFrame.IsOverlapping(otherCollider.BoundingBoxNextFrame))
                {
                    _collidersCollidingWithThisFrame.Add(otherCollider);
                    if (otherCollider.IsTrigger || IsTrigger)
                    {
                        GameObject.OnTriggeredBy(otherCollider);
                    }
                    else
                    {
                        var directionOfCollision = BoundingBoxCurrentFrame.DirectionRelativeTo(otherCollider.BoundingBoxCurrentFrame);
                        _physicsBody.OnCollisionWith(directionOfCollision);
                        GameObject.OnCollidedWith(otherCollider);
                    }
                }
            }
            foreach (var collider in _collidersCollidingWithLastFrame.Where(collider => !_collidersCollidingWithThisFrame.Contains(collider)))
                if (collider.IsTrigger || IsTrigger)
                    GameObject.OnStoppedBeingTriggeredBy(collider);
                else
                    GameObject.OnStoppedCollidingWith(collider);
        }
    }
}