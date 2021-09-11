using N8Engine.Animation;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace SampleProject
{
    public sealed class PlayerAnimationController
    {
        private readonly PlayerInputs _input;
        private readonly Animator _animator;
        private readonly PlayerWalkAnimation _walkAnimation = new();
        private readonly FlippedPlayerWalkAnimation _flippedWalkAnimation = new();
        private readonly PlayerIdleAnimation _idleAnimation = new();
        private readonly FlippedPlayerIdleAnimation _flippedIdleAnimation = new();
        private readonly PlayerJumpAnimation _jumpAnimation = new();
        private readonly FlippedPlayerJumpAnimation _flippedJumpAnimation = new();
        private readonly PlayerJumpAnimation _fallAnimation = new();
        private readonly FlippedPlayerJumpAnimation _flippedFallAnimation = new();

        public PlayerAnimationController(Animator animator, PlayerInputs input)
        {
            _animator = animator;
            _animator.ChangeAnimation(_idleAnimation);
            _input = input;
        }

        public void HandleWalkingAnimation(bool isGrounded)
        {
            var wasFacingRight = _input.LastDirectionWhenThereWasInput == Direction.Right;
            var wasFacingLeft = _input.LastDirectionWhenThereWasInput == Direction.Left;
            
            if (isGrounded)
                _animator.Animation = _input.CurrentDirection switch
                {
                    Direction.Right => _walkAnimation,
                    Direction.Left => _flippedWalkAnimation,
                    Direction.None when wasFacingRight => _idleAnimation,
                    Direction.None when wasFacingLeft => _flippedIdleAnimation,
                    var _ => _animator.Animation
                };
            else
                _animator.Animation = _input.CurrentDirection switch
                {
                    Direction.Right => _fallAnimation,
                    Direction.Left => _flippedFallAnimation,
                    Direction.None when wasFacingRight => _jumpAnimation,
                    Direction.None when wasFacingLeft => _flippedJumpAnimation,
                    var _ => _animator.Animation
                };
        }
        
        public void HandleLandAnimation()
        {
            var isWalking = _input.CurrentDirection != Direction.None;
            _animator.Animation = _input.LastDirectionWhenThereWasInput switch
            {
                Direction.Right when isWalking => _walkAnimation,
                Direction.Left when isWalking => _flippedWalkAnimation,
                Direction.Right => _idleAnimation,
                Direction.Left => _flippedIdleAnimation,
                var _ => _idleAnimation
            };
        }
    }
}