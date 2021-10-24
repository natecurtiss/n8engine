using N8Engine;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace SampleProject
{
    public sealed class Player : GameObject
    {
        private const int SPEED = 50;
        private const int JUMP_FORCE = 200;
        private readonly PlayerInputs _input = new();

        private PlayerAnimationController _animationController;
        private GroundCheck<ICanBeJumpedOn> _groundCheck;
        private Vector _spawnPosition;

        private bool CanJump => _groundCheck.IsGrounded && _input.JustPressedJumpButton;

        protected override void OnStart()
        {
            _animationController = new PlayerAnimationController(Animator, _input, SpriteRenderer);
            _groundCheck = Create<GroundCheck<ICanBeJumpedOn>>("player ground check");
            _groundCheck.OnLandedOnTheGround += _animationController.HandleLandAnimation;
            _groundCheck.Collider.Size = new Vector(5f, 3f);
            _groundCheck.Collider.Offset = new Vector(1f, -3f);

            Collider.Size = new Vector(8, 8);
            SpriteRenderer.SortingOrder = -2;
            PhysicsBody.UseGravity = true;
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
            if (Transform.Position.Y <= -100f) Die();
        }
        
        public Player Spawn()
        {
            Transform.Position = _spawnPosition = Window.LeftSide + Vector.Right * 10f;
            return this;
        }

        private void Move()
        {
            var horizontalInput = _input.CurrentDirection.AsVector().X;
            PhysicsBody.Velocity = new Vector(horizontalInput * SPEED, PhysicsBody.Velocity.Y);
        }

        private void Jump()
        {
            PhysicsBody.Velocity = new Vector(PhysicsBody.Velocity.X, JUMP_FORCE);
            _groundCheck.IsGrounded = false;
            _animationController.Jump();
        }

        private void ClampPositionWithinWindow()
        {
            var position = Transform.Position;
            var offset = Collider.Size.X / 2f;
            position.X = position.X.ClampedBetween(Window.LeftSide.X + offset, Window.RightSide.X - offset);
            Transform.Position = position;
        }

        private void Die()
        {
            Transform.Position = _spawnPosition;
            PhysicsBody.Velocity = Vector.Zero;
        }
    }
}