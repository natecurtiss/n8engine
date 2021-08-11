using N8Engine.Mathematics;
using N8Engine.SceneManagement;

namespace N8Engine.Physics
{
    public sealed class Collider
    {
        private const float GRAVITY = 9.82f;

        public readonly GameObject GameObject;
        internal readonly DebugCollider DebugMode;
        private readonly Transform _transform;
        
        private Vector _size;

        public Vector Velocity { get; set; }
        public bool UseGravity { get; set; }
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
        
        private Vector Position => _transform.Position + Offset;
        private BoundingBox BoundingBoxCurrentFrame { get; set; }
        private BoundingBox BoundingBoxNextFrame { get; set; }

        internal Collider(GameObject gameObject)
        {
            GameObject = gameObject;
            _transform = gameObject.Transform;
            DebugMode = new DebugCollider(Size, Position);
            GameLoop.OnPostUpdate += OnPostUpdate;
            GameLoop.OnPhysicsUpdate += OnPhysicsUpdate;
        }

        internal void Destroy()
        {
            GameLoop.OnPostUpdate -= OnPostUpdate;
            GameLoop.OnPhysicsUpdate -= OnPhysicsUpdate;
        }

        private void OnPostUpdate(float deltaTime)
        {
            if (UseGravity)
                Velocity += Vector.Up * GRAVITY;
            BoundingBoxCurrentFrame = new BoundingBox(Size, Position);
            BoundingBoxNextFrame = new BoundingBox(Size, Position + Velocity * deltaTime);
        }

        private void OnPhysicsUpdate(float deltaTime)
        {
            foreach (var otherGameObject in SceneManager.CurrentScene.GameObjects)
            {
                var otherCollider = otherGameObject.Collider;
                if (otherCollider == this) continue;
                if (otherCollider.Size == Vector.Zero) continue;
                if (BoundingBoxNextFrame.IsOverlapping(otherCollider.BoundingBoxNextFrame))
                {
                    var directionOfCollision = BoundingBoxCurrentFrame.DirectionRelativeTo(otherCollider.BoundingBoxCurrentFrame);
                    Debug.Log(directionOfCollision);
                    Velocity = new Vector
                    (
                        directionOfCollision is Direction.Left or Direction.Right ? 0f : Velocity.X,
                        directionOfCollision is Direction.Top or Direction.Down ? 0f : Velocity.Y
                    );
                    if (directionOfCollision != Direction.None)
                    {
                        if (otherCollider.IsTrigger || IsTrigger)
                            GameObject.TriggeredWith(otherCollider);
                        else
                            GameObject.CollidedWith(otherCollider);
                    }
                    break;
                }
            }
            _transform.Position += Velocity * deltaTime;
            DebugMode.Position = Position;
        }
    }
}