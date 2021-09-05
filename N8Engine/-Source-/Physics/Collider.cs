using System.Collections.Generic;
using System.Linq;
using N8Engine.Mathematics;
using N8Engine.Rendering;
using N8Engine.SceneManagement;

namespace N8Engine.Physics
{
    public sealed class Collider : Component
    {
        private readonly ColliderVisualization _visualization;
        private readonly List<Collider> _collidersCollidingWithThisFrame = new();
        private readonly List<Collider> _collidersCollidingWithLastFrame = new();
        private readonly PhysicsBody _physicsBody;
        private Vector _size;

        public Transform Transform => GameObject.Transform;
        public IEnumerable<Collider> Contacts => _collidersCollidingWithThisFrame;
        public bool IsVisible { get; set; }
        public bool IsTrigger { get; set; }
        public Vector Offset { get; set; }
        public Vector Size
        {
            get => _size;
            set
            {
                if (_size == value) return;
                _visualization.Redraw(value);
                _size = value;
            }
        }
        internal Vector Position => Transform.Position + Offset;
        internal Sprite Sprite => Size == Vector.Zero ? Sprite.Empty : _visualization.Sprite;

        private BoundingBox BoundingBoxThisFrame { get; set; }
        private BoundingBox BoundingBoxNextFrame { get; set; }

        internal Collider(GameObject gameObject) : base(gameObject)
        {
            _visualization = new ColliderVisualization(this);
            _physicsBody = gameObject.PhysicsBody;
        }

        internal void UpdateBoundingBoxes(float deltaTime)
        {
            BoundingBoxThisFrame = new BoundingBox(GameObject.Name, Size, Position);
            BoundingBoxNextFrame = new BoundingBox(GameObject.Name, Size, Position + _physicsBody.Velocity * deltaTime);
        }

        internal void CheckCollisions()
        {
            _collidersCollidingWithLastFrame.Clear();
            foreach (var collider in _collidersCollidingWithThisFrame)
                _collidersCollidingWithLastFrame.Add(collider);
            _collidersCollidingWithThisFrame.Clear();

            if (Size == Vector.Zero) return;
            foreach (var otherGameObject in SceneManager.CurrentScene.ToArray())
            {
                var otherCollider = otherGameObject.Collider;
                if (otherCollider == this) continue;
                if (otherCollider.Size == Vector.Zero) continue;

                if (BoundingBoxNextFrame.IsOverlapping(otherCollider.BoundingBoxNextFrame))
                {
                    _collidersCollidingWithThisFrame.Add(otherCollider);
                    if (otherCollider.IsTrigger || IsTrigger)
                    {
                        if (!_collidersCollidingWithLastFrame.Contains(otherCollider)) 
                            GameObject.OnTriggeredBy(otherCollider);
                    }
                    else
                    {
                        var directionOfCollision = BoundingBoxThisFrame.DirectionRelativeTo(otherCollider.BoundingBoxThisFrame);
                        _physicsBody.OnCollisionWith(directionOfCollision);
                        if (!_collidersCollidingWithLastFrame.Contains(otherCollider)) 
                            GameObject.OnCollidedWith(otherCollider);
                    }
                }
            }
            foreach (var collider in _collidersCollidingWithLastFrame.ToArray())
                if (!_collidersCollidingWithThisFrame.Contains(collider))
                    if (collider.IsTrigger || IsTrigger)
                        GameObject.OnStoppedBeingTriggeredBy(collider);
                    else
                        GameObject.OnStoppedCollidingWith(collider);
        }
    }
}