using N8Engine;
using N8Engine.Inputs;
using N8Engine.Mathematics;
using N8Engine.Physics;

namespace SampleProject
{
    internal sealed class Player : GameObject
    {
        private const int SPEED = 2600;
        private const int JUMP_FORCE = -200;

        private readonly WasdAndArrowKeyInputs _inputs = new();
        private readonly PlayerWalkAnimation _walkAnimation = new();
        private readonly FlippedPlayerWalkAnimation _flippedWalkAnimation = new();
        private readonly PlayerIdleAnimation _idleAnimation = new();
        private readonly FlippedPlayerIdleAnimation _flippedIdleAnimation = new();
        private readonly PlayerJumpAnimation _jumpAnimation = new();
        private readonly FlippedPlayerJumpAnimation _flippedJumpAnimation = new();

        private bool _isGrounded;
        private Direction _currentDirection = Direction.Right;

        protected override void OnStart()
        {
            Collider.Size = new Vector(10f, 7f);
            Collider.Offset = Vector.Down;
            PhysicsBody.UseGravity = true;
            AnimationPlayer.Animation = _idleAnimation;
            AnimationPlayer.Play();
        }

        protected override void OnUpdate(float deltaTime)
        {
            UpdateDirection();
            HandleAnimations();
            Move(deltaTime);
            if (Key.Spacebar.WasJustPressed() && _isGrounded)
                Jump();
        }
        
        public override void OnCollidedWith(Collider otherCollider)
        {
            var isAirborne = !_isGrounded;
            if (isAirborne && otherCollider.GameObject.Is<Floor>())
            {
                _isGrounded = true;
                AnimationPlayer.Animation = _currentDirection switch
                {
                    Direction.Left => _flippedIdleAnimation,
                    Direction.Right => _idleAnimation,
                    var _ => _idleAnimation
                };
            }
        }

        private void UpdateDirection()
        {
            _currentDirection = _inputs.InputVector.X switch
            {
                > 0 => Direction.Right,
                < 0 => Direction.Left,
                var _ => _currentDirection
            };
        }

        private void HandleAnimations()
        {
            const int is_not_pressing_a_key = 0;
            if (_isGrounded)
                AnimationPlayer.Animation = _inputs.InputVector.X switch
                {
                    > 0 => _walkAnimation,
                    < 0 => _flippedWalkAnimation,
                    is_not_pressing_a_key when _currentDirection == Direction.Right => _idleAnimation,
                    is_not_pressing_a_key when _currentDirection == Direction.Left => _flippedIdleAnimation,
                    var _ => AnimationPlayer.Animation
                };
            else
                AnimationPlayer.Animation = _inputs.InputVector.X switch
                {
                    > 0 => _jumpAnimation,
                    < 0 => _flippedJumpAnimation,
                    is_not_pressing_a_key when _currentDirection == Direction.Right => _jumpAnimation,
                    is_not_pressing_a_key when _currentDirection == Direction.Left => _flippedJumpAnimation,
                    var _ => AnimationPlayer.Animation
                };
        }
        
        private void Move(float deltaTime) => PhysicsBody.Velocity = new Vector(_inputs.InputVector.X * SPEED * deltaTime, PhysicsBody.Velocity.Y);

        private void Jump()
        {
            PhysicsBody.Velocity = new Vector(PhysicsBody.Velocity.X, JUMP_FORCE);
            _isGrounded = false;
        }
    }
}