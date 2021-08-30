using System;
using N8Engine;

namespace SampleProject
{
    public sealed class GroundCheck<TGround> : GameObject where TGround : IGameObjectInterface
    {
        public event Action OnLandedOnTheGround;
        public event Action OnJumpedOffTheGround;

        private const float COYOTE_TIME = 0.3f;
        private float _groundedTimer;
        
        public bool IsGrounded
        {
            get => _groundedTimer > 0f;
            set => _groundedTimer = value ? COYOTE_TIME : 0f;
        }

        protected override void OnStart() => Collider.IsTrigger = true;

        protected override void OnEarlyUpdate(float deltaTime)
        {
            var isGroundedThisFrame = false;
            var wasGroundedLastFrame = IsGrounded;
            foreach (var collider in Collider.Contacts)
                if (collider.GameObject.Is<TGround>())
                {
                    isGroundedThisFrame = true;
                    break;
                }
            _groundedTimer -= deltaTime;
            if (isGroundedThisFrame)
            {
                IsGrounded = true;
                if (!wasGroundedLastFrame)
                    OnLandedOnTheGround?.Invoke();
            }
            else
            {
                if (wasGroundedLastFrame && !IsGrounded)
                    OnJumpedOffTheGround?.Invoke();
            }

        }
    }
}