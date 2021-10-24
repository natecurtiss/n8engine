using N8Engine;
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
        private readonly PlayerWalkAnimation _walk;
        private readonly PlayerIdleAnimation _idle;
        private readonly PlayerJumpAnimation _jump;
        
        private bool IsWalking => _input.CurrentDirection is Right or Left;

        public PlayerAnimationController(Animator animator, PlayerInputs input, SpriteRenderer spriteRenderer)
        {
            _idle = Animation.Create<PlayerIdleAnimation>();
            _walk = Animation.Create<PlayerWalkAnimation>();
            _jump = Animation.Create<PlayerJumpAnimation>();
            _animator = animator;
            _animator.Play(_idle);
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
                if (IsWalking) _animator.Play(_walk);
                else _animator.Play(_idle);
            }
        }

        public void Jump() => _animator.Play(_jump);

        public void HandleLandAnimation()
        {
            if (IsWalking) _animator.Play(_walk);
            else _animator.Play(_idle);
        }
    }
}