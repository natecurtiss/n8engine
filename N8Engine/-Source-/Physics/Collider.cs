using N8Engine.Mathematics;
using N8Engine.SceneManagement;

namespace N8Engine.Physics
{
    public sealed class Collider : Component
    {
        internal readonly DebugCollider DebugMode;
        
        private Vector _size;

        public bool IsDebugModeEnabled { get; set; }
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
        private BoundingBox BoundingBoxCurrentFrame { get; set; }
        private BoundingBox BoundingBoxNextFrame { get; set; }

        internal Collider(GameObject gameObject) : base(gameObject) => DebugMode = new DebugCollider(this);

        internal void UpdateBoundingBoxes(float deltaTime)
        {
            BoundingBoxCurrentFrame = new BoundingBox(Size, Position);
            BoundingBoxNextFrame = new BoundingBox(Size, Position + PhysicsBody.Velocity * deltaTime);
        }

        internal void CheckCollisions()
        {
            foreach (var otherGameObject in SceneManager.CurrentScene.GameObjects)
            {
                var otherCollider = otherGameObject.Collider;
                if (otherCollider == this) continue;
                if (otherCollider.Size == Vector.Zero) continue;
                if (BoundingBoxNextFrame.IsOverlapping(otherCollider.BoundingBoxNextFrame))
                {
                    if (otherCollider.IsTrigger || IsTrigger)
                    {
                        GameObject.OnTriggeredBy(otherCollider);
                    }
                    else
                    {
                        var directionOfCollision = BoundingBoxCurrentFrame.DirectionRelativeTo(otherCollider.BoundingBoxCurrentFrame);
                        var wasCollision = directionOfCollision != Direction.None;
                        if (wasCollision)
                        {
                            PhysicsBody.OnCollisionWith(directionOfCollision);
                            GameObject.OnCollidedWith(otherCollider);
                        }
                    }
                }
            }
        }
    }
}