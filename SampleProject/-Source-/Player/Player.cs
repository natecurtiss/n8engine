using N8Engine;
using N8Engine.Mathematics;
using N8Engine.Physics;

namespace SampleProject
{
    internal sealed class Player : GameObject
    {
        private readonly WasdAndArrowKeyInputs _inputs = new();
        private readonly PlayerWalkAnimation _walkAnimation = new();
        private readonly FlippedPlayerWalkAnimation _flippedWalkAnimation = new();
        private readonly PlayerIdleAnimation _idleAnimation = new();
        private readonly FlippedPlayerIdleAnimation _flippedIdleAnimation = new();
        
        protected override void OnStart()
        {
            Collider.Size = new Vector(10f, 7f);
            Collider.Offset = Vector.Down;
            Collider.UseGravity = true;
            AnimationPlayer.Animation = _idleAnimation;
            AnimationPlayer.Play();
        }

        protected override void OnUpdate(float deltaTime)
        {
            AnimationPlayer.Animation = _inputs.InputVector.X switch
            {
                > 0 => _walkAnimation,
                < 0 => _flippedWalkAnimation,
                0 when AnimationPlayer.Animation == _walkAnimation => _idleAnimation,
                0 when AnimationPlayer.Animation == _flippedWalkAnimation => _flippedIdleAnimation,
                _ => AnimationPlayer.Animation
            };
            Collider.Velocity = new Vector(_inputs.InputVector.X * 2250 * deltaTime, Collider.Velocity.Y);
        }

        protected override void OnCollision(Collider otherCollider)
        {
            Debug.Log(otherCollider.GameObject.Name);
        }
    }
}