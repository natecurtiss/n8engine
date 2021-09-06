using N8Engine.Mathematics;

namespace N8Engine.Physics
{
    /// <summary>
    /// The <see cref="Component"/> of a <see cref="GameObject"/> that handles physics.
    /// </summary>
    /// <seealso cref="Collider"/> <seealso cref="PhysicsSettings"/>
    public sealed class PhysicsBody : Component
    {
        private const float GRAVITY = 9.82f;
        private const float GRAVITY_SCALE = 50f;

        /// <summary>
        /// The <see cref="Transform"/> attached to the same <see cref="GameObject"/> as the <see cref="PhysicsBody">PhysicsBody.</see>
        /// </summary>
        public Transform Transform => GameObject.Transform;
        /// <summary>
        /// The speed of the <see cref="PhysicsBody"/> - set this to move with physics and not ignore collisions.
        /// </summary>
        public Vector Velocity { get; set; }
        /// <summary>
        /// Gravity is enabled if true...imagine that.
        /// </summary>
        public bool UseGravity { get; set; }

        internal PhysicsBody(GameObject gameObject) : base(gameObject) { }

        internal void ApplyGravity(float deltaTime)
        {
            if (UseGravity)
                Velocity += Vector.Down * GRAVITY * GRAVITY_SCALE * deltaTime;
        }

        internal void OnCollisionWith(Direction directionOfCollision)
        {
            Velocity = new Vector
            (
                directionOfCollision is Direction.Left or Direction.Right ? 0f : Velocity.X,
                directionOfCollision is Direction.Up or Direction.Down ? 0f : Velocity.Y
            );
        }

        internal void ApplyVelocity(float deltaTime)
        {
            Transform.Position += Velocity * deltaTime;
        }
    }
}