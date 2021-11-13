using N8Engine.Mathematics;

namespace N8Engine.Inputs
{
    public sealed class PlatformerInput : Component
    {
        readonly float _coyoteTime;
        float _jumpTimer;

        public int Horizontal { get; private set; }
        public bool CanJump => _jumpTimer > 0f;

        public PlatformerInput(float coyoteTime) => _coyoteTime = coyoteTime;

        protected override void OnEarlyUpdate(Time time)
        {
            _jumpTimer = Math.Max(0, _jumpTimer - time.DeltaTime);
        }
    }
}