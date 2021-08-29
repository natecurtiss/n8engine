using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace SampleProject
{
    public sealed class PlayerAnimationController
    {
        readonly PlayerInputs _input;
        readonly AnimationPlayer _animationPlayer;
        readonly PlayerWalkAnimation _walkAnimation = new();
        readonly FlippedPlayerWalkAnimation _flippedWalkAnimation = new();
        readonly PlayerIdleAnimation _idleAnimation = new();
        readonly FlippedPlayerIdleAnimation _flippedIdleAnimation = new();
        readonly PlayerJumpAnimation _jumpAnimation = new();
        readonly FlippedPlayerJumpAnimation _flippedJumpAnimation = new();
        readonly PlayerJumpAnimation _fallAnimation = new();
        readonly FlippedPlayerJumpAnimation _flippedFallAnimation = new();

        public PlayerAnimationController(AnimationPlayer animationPlayer, PlayerInputs input)
        {
            _animationPlayer = animationPlayer;
            _animationPlayer.Animation = _idleAnimation;
            _input = input;
        }

        public void HandleWalkingAnimation(bool isGrounded)
        {
            var wasFacingRight = _input.LastDirectionWhenThereWasInput == Direction.Right;
            var wasFacingLeft = _input.LastDirectionWhenThereWasInput == Direction.Left;
            
            if (isGrounded)
                _animationPlayer.Animation = _input.CurrentDirection switch
                {
                    Direction.Right => _walkAnimation,
                    Direction.Left => _flippedWalkAnimation,
                    Direction.None when wasFacingRight => _idleAnimation,
                    Direction.None when wasFacingLeft => _flippedIdleAnimation,
                    var _ => _animationPlayer.Animation
                };
            else
                _animationPlayer.Animation = _input.CurrentDirection switch
                {
                    Direction.Right => _fallAnimation,
                    Direction.Left => _flippedFallAnimation,
                    Direction.None when wasFacingRight => _jumpAnimation,
                    Direction.None when wasFacingLeft => _flippedJumpAnimation,
                    var _ => _animationPlayer.Animation
                };
        }
        
        public void HandleLandAnimation()
        {
            var isWalking = _input.CurrentDirection != Direction.None;
            _animationPlayer.Animation = _input.LastDirectionWhenThereWasInput switch
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