using N8Engine.Mathematics;

namespace N8Engine.Physics
{
    public sealed class PhysicsBody : Component
    {
        const float GRAVITY = 9.82f;
        const float GRAVITY_SCALE = 50f;

        public Transform Transform => GameObject.Transform;
        public Vector Velocity { get; set; }
        public bool UseGravity { get; set; }

        internal PhysicsBody(GameObject gameObject) : base(gameObject) { }

        internal void ApplyGravity(float deltaTime)
        {
            if (UseGravity)
                Velocity += Vector.Up * GRAVITY * GRAVITY_SCALE * deltaTime;
        }

        internal void OnCollisionWith(Direction directionOfCollision)
        {
            if (directionOfCollision == Direction.None) 
                Velocity = Vector.Zero;
            else
                Velocity = new Vector
                (
                    directionOfCollision is Direction.Left or Direction.Right ? 0f : Velocity.X,
                    directionOfCollision is Direction.Up or Direction.Down ? 0f : Velocity.Y
                );
        }

        internal void ApplyVelocity(float deltaTime) => Transform.Position += Velocity * deltaTime;
    }
}