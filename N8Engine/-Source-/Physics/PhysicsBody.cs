using N8Engine.Mathematics;

namespace N8Engine.Physics
{
    public sealed class PhysicsBody : Component
    {
        private const float GRAVITY = 9.82f;

        public Vector Velocity { get; set; }
        public bool UseGravity { get; set; }

        internal PhysicsBody(GameObject gameObject) : base(gameObject) { }

        internal void ApplyGravity()
        {
            if (UseGravity)
                Velocity += Vector.Up * GRAVITY;
        }

        internal void OnCollisionWith(Direction directionOfCollision)
        {
            Velocity = new Vector
            (
                directionOfCollision is Direction.Left or Direction.Right ? 0f : Velocity.X,
                directionOfCollision is Direction.Top or Direction.Down ? 0f : Velocity.Y
            );
        }
        
        internal void ApplyVelocity(float deltaTime) => Transform.Position += Velocity * deltaTime;
    }
}