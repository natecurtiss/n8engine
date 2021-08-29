using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace SampleProject
{
    public sealed class PlayerAnimationController
    {
        readonly AnimationPlayer _animationPlayer;
        readonly PlayerWalkAnimation _walkAnimation = new();
        readonly FlippedPlayerWalkAnimation _flippedWalkAnimation = new();
        readonly PlayerIdleAnimation _idleAnimation = new();
        readonly FlippedPlayerIdleAnimation _flippedIdleAnimation = new();
        readonly PlayerJumpAnimation _jumpAnimation = new();
        readonly FlippedPlayerJumpAnimation _flippedJumpAnimation = new();
        readonly PlayerJumpAnimation _fallAnimation = new();
        readonly FlippedPlayerJumpAnimation _flippedFallAnimation = new();

        public PlayerAnimationController(AnimationPlayer animationPlayer)
        {
            _animationPlayer = animationPlayer;
            _animationPlayer.Animation = _idleAnimation;
        }

        public void HandleWalkingAnimation(bool isGrounded, Direction currentDirectionOfInput, Direction lastDirectionOfInput)
        {
            var wasFacingRight = lastDirectionOfInput == Direction.Right;
            var wasFacingLeft = lastDirectionOfInput == Direction.Left;
            
            if (isGrounded)
                _animationPlayer.Animation = currentDirectionOfInput switch
                {
                    Direction.Right => _walkAnimation,
                    Direction.Left => _flippedWalkAnimation,
                    Direction.None when wasFacingRight => _idleAnimation,
                    Direction.None when wasFacingLeft => _flippedIdleAnimation,
                    var _ => _animationPlayer.Animation
                };
            else
                _animationPlayer.Animation = currentDirectionOfInput switch
                {
                    Direction.Right => _fallAnimation,
                    Direction.Left => _flippedFallAnimation,
                    Direction.None when wasFacingRight => _jumpAnimation,
                    Direction.None when wasFacingLeft => _flippedJumpAnimation,
                    var _ => _animationPlayer.Animation
                };
        }
        
        public void HandleLandAnimation(Direction currentDirectionOfInput, Direction lastDirectionOfInput)
        {
            var isWalking = currentDirectionOfInput != Direction.None;
            _animationPlayer.Animation = lastDirectionOfInput switch
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