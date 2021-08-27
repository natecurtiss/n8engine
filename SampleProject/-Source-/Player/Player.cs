using System;
using N8Engine;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace SampleProject
{
    public sealed class Player : GameObject
    {
        private const int SPEED = 100;
        private const int JUMP_FORCE = -200;

        private PlayerAnimationController _animationController;
        private GroundCheck<ICanBeJumpedOn> _groundCheck;
        private PlayerInputs _inputs;
        private Vector<Direction, Direction> _lastDirectionOfInput;

        private bool CanJump => _groundCheck.IsGrounded && _inputs.JustPressedJumpButton;
        private bool IsWalking => _inputs.Direction.First != 0f;
        private Vector SpawnPosition => Window.LeftSide + Vector.Right * 15f;

        protected override void OnStart()
        {
            Transform.Position = SpawnPosition;

            _animationController = new PlayerAnimationController(AnimationPlayer);
            _groundCheck = Create<GroundCheck<ICanBeJumpedOn>>("player ground check");
            _groundCheck.OnLandedOnTheGround += Land;
            _groundCheck.Collider.Size = new Vector(10f, 1f);
            _groundCheck.Collider.Offset = Vector.Up * 5f;
            _inputs = Create<PlayerInputs>("player inputs");
            
            Collider.Size = new Vector(10f, 7f);
            Collider.Offset = Vector.Right;
            SpriteRenderer.SortingOrder = 1;
            PhysicsBody.UseGravity = true;
            AnimationPlayer.Play();
        }

        protected override void OnDestroy() => _groundCheck.OnLandedOnTheGround -= Land;

        protected override void OnUpdate(float deltaTime)
        {
            Move();
            if (CanJump) Jump();
            HandleWalkingAnimations();
        }

        protected override void OnLateUpdate(float deltaTime)
        {
            ClampPositionWithinWindow();
            _groundCheck.Transform.Position = Transform.Position;
            UpdateLastDirection();
            if (Transform.Position.Y >= Window.BottomSide.Y) Die();
        }

        private void UpdateLastDirection()
        {
            _lastDirectionOfInput.First = _inputs.Direction.First switch
            {
                > 0 => Direction.Right,
                < 0 => Direction.Left,
                var _ => _lastDirectionOfInput.First
            };
            _lastDirectionOfInput.Second = _inputs.Direction.Second switch
            {
                > 0 => Direction.Up,
                < 0 => Direction.Down,
                var _ => _lastDirectionOfInput.Second
            };
        }

        private void Move()
        {
            var horizontalInput = _inputs.Direction.First.AsVector().X;
            PhysicsBody.Velocity = new Vector(horizontalInput * SPEED, PhysicsBody.Velocity.Y);
        }

        private void Jump()
        {
            PhysicsBody.Velocity = new Vector(PhysicsBody.Velocity.X, JUMP_FORCE);
            _groundCheck.IsGrounded = false;
        }

        private void Land() => _animationController.HandleLandAnimation
            (_inputs.Direction.First, _lastDirectionOfInput.First);

        private void HandleWalkingAnimations() => _animationController.HandleWalkingAnimation
                (_groundCheck.IsGrounded, _inputs.Direction.First, _lastDirectionOfInput.First);

        private void ClampPositionWithinWindow()
        {
            var position = Transform.Position;
            var offset = Collider.Size.X / 2f + 5f;
            position.X = position.X.ClampedBetween(Window.LeftSide.X + offset, Window.RightSide.X - offset);
            Transform.Position = position;
        }

        private void Die()
        {
            Transform.Position = SpawnPosition;
            PhysicsBody.Velocity = Vector.Zero;
        }
    }
}