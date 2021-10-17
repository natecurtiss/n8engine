using N8Engine.Animation;
using N8Engine.Mathematics;
using N8Engine.Rendering;
using static N8Engine.Mathematics.Direction;

namespace SampleProject
{
    public sealed class PlayerAnimationController
    {
        private readonly PlayerInputs _input;
        private readonly Animator _animator;
        private readonly SpriteRenderer _spriteRenderer;
        private readonly PlayerWalkAnimation _walk = Animation.Create<PlayerWalkAnimation>();
        private readonly PlayerIdleAnimation _idle = Animation.Create<PlayerIdleAnimation>();
        private readonly PlayerJumpAnimation _airborne = Animation.Create<PlayerJumpAnimation>();
        
        private bool IsWalking => _input.CurrentDirection is Right or Left;

        public PlayerAnimationController(Animator animator, PlayerInputs input, SpriteRenderer spriteRenderer)
        {
            _animator = animator;
            _animator.ChangeAnimation(_idle);
            _input = input;
            _spriteRenderer = spriteRenderer;
        }

        public void HandleWalkingAnimation(bool isGrounded)
        {
            _spriteRenderer.ShouldFlip = _input.CurrentDirection switch
            {
                Right => Flip.None,
                Left => Flip.Horizontal,
                var _ => _spriteRenderer.ShouldFlip
            };
            if (isGrounded)
            {
                if (IsWalking) _animator.ChangeAnimation(_walk);
                else _animator.ChangeAnimation(_idle);
            }
            else
            {
                _animator.ChangeAnimation(_airborne);
            }
        }
        
        public void HandleLandAnimation()
        {
            if (IsWalking) _animator.ChangeAnimation(_walk);
            else _animator.ChangeAnimation(_idle);
        }
    }
}