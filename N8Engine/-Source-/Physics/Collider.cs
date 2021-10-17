using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using N8Engine.Mathematics;
using N8Engine.Rendering;
using N8Engine.SceneManagement;

namespace N8Engine.Physics
{
    /// <summary>
    /// The <see cref="Component"/> of a <see cref="GameObject"/> that handles collision.
    /// </summary>
    /// <seealso cref="PhysicsBody"/> <seealso cref="PhysicsSettings"/>
    public sealed class Collider : Component
    {
        private readonly Color _visualizationColor = Color.Lime;
        private readonly List<Collider> _collidersCollidingWithThisFrame = new();
        private readonly List<Collider> _collidersCollidingWithLastFrame = new();
        private readonly PhysicsBody _physicsBody;
        
        private Vector _size;
        private Sprite _visualization;

        /// <summary>
        /// The <see cref="Transform"/> attached to the same <see cref="GameObject"/> as the <see cref="Collider">Collider.</see>
        /// </summary>
        public Transform Transform => GameObject.Transform;
        /// <summary>
        /// Every <see cref="Collider"/> regardless of if <see cref="IsTrigger"/> is true or false colliding with this <see cref="Collider"/> in the current frame.
        /// </summary>
        public IEnumerable<Collider> Contacts => _collidersCollidingWithThisFrame;
        /// <summary>
        /// Shows a debug visualization of the collider if true.
        /// </summary>
        public bool IsVisible { get; set; }
        /// <summary>
        /// If true: other <see cref="Collider">Colliders</see> are able to pass through ignoring any collisions and trigger
        /// <see cref="GameObject.OnTriggeredBy"/> and <see cref="GameObject.OnStoppedBeingTriggeredBy"/> event methods.
        /// If false (default): other <see cref="Collider">Colliders</see> with <see cref="IsTrigger"/> equal to false are stopped when colliding with this and
        /// trigger <see cref="GameObject.OnCollidedWith"/> and <see cref="GameObject.OnStoppedCollidingWith"/> event methods.
        /// </summary>
        public bool IsTrigger { get; set; }
        /// <summary>
        /// The <see cref="Vector">offset</see> of <see cref="Position">Collider.Position</see> from <see cref="N8Engine.Mathematics.Transform.Position">Transform.Position.</see>
        /// </summary>
        public Vector Offset { get; set; }
        /// <summary>
        /// <see cref="N8Engine.Mathematics.Transform.Position">Transform.Position</see> + <see cref="Offset">Collider.Offset.</see>
        /// </summary>
        public Vector Position => Transform.Position + Offset;
        /// <summary>
        /// The size of the <see cref="Collider">Collider.</see>
        /// </summary>
        public Vector Size
        {
            get => _size;
            set
            {
                if (_size == value) return;
                _visualization = new OutlinedRectangle(_visualizationColor, value);
                _size = value;
            }
        }
        internal Sprite Sprite => Size == Vector.Zero ? Sprite.Empty : _visualization;

        private BoundingBox BoundingBoxThisFrame { get; set; }
        private BoundingBox BoundingBoxNextFrame { get; set; }

        internal Collider(GameObject gameObject) : base(gameObject)
        {
            _visualization = new OutlinedRectangle(_visualizationColor, Size);
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