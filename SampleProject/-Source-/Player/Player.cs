using N8Engine;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace SampleProject
{
    public sealed class Player : GameObject
    {
        const int SPEED = 100;
        const int JUMP_FORCE = -200;

        readonly PlayerInputs _input = new();
        PlayerAnimationController _animationController;
        GroundCheck<ICanBeJumpedOn> _groundCheck;

        bool CanJump => _groundCheck.IsGrounded && _input.JustPressedJumpButton;
        Vector SpawnPosition => Window.LeftSide + Vector.Right * 15f;

        protected override void OnStart()
        {
            _animationController = new PlayerAnimationController(AnimationPlayer, _input);
            _groundCheck = Create<GroundCheck<ICanBeJumpedOn>>("player ground check");
            _groundCheck.OnLandedOnTheGround += _animationController.HandleLandAnimation;
            _groundCheck.Collider.Size = new Vector(10f, 1f);
            _groundCheck.Collider.Offset = Vector.Up * 5f;

            Transform.Position = SpawnPosition;
            Collider.Size = new Vector(10f, 7f);
            Collider.Offset = Vector.Right;
            SpriteRenderer.SortingOrder = -2;
            PhysicsBody.UseGravity = true;
            AnimationPlayer.Play();
        }

        protected override void OnDestroy() => _groundCheck.OnLandedOnTheGround -= _animationController.HandleLandAnimation;

        protected override void OnUpdate(float deltaTime)
        {
            _input.GetInputs(deltaTime);
            Move();
            if (CanJump) Jump();
            _animationController.HandleWalkingAnimation(_groundCheck.IsGrounded);
        }

        protected override void OnLateUpdate(float deltaTime)
        {
            ClampPositionWithinWindow();
            _groundCheck.Transform.Position = Transform.Position;
            if (Transform.Position.Y >= Window.BottomSide.Y) Die();
        }

        void Move()
        {
            var horizontalInput = _input.CurrentDirection.AsVector().X;
            PhysicsBody.Velocity = new Vector(horizontalInput * SPEED, PhysicsBody.Velocity.Y);
        }

        void Jump()
        {
            PhysicsBody.Velocity = new Vector(PhysicsBody.Velocity.X, JUMP_FORCE);
            _groundCheck.IsGrounded = false;
        }

        void ClampPositionWithinWindow()
        {
            var position = Transform.Position;
            var offset = Collider.Size.X / 2f + 5f;
            position.X = position.X.ClampedBetween(Window.LeftSide.X + offset, Window.RightSide.X - offset);
            Transform.Position = position;
        }

        void Die()
        {
            Transform.Position = SpawnPosition;
            PhysicsBody.Velocity = Vector.Zero;
        }
    }
}