namespace N8Engine.InputSystem
{
    public sealed class PlatformerInput : Component
    {
        readonly float _jumpBuffer;
        readonly Keybind _left;
        readonly Keybind _right;
        readonly Keybind _jump;
        
        Input _input;
        float _jumpTimer;

        public int Horizontal
        {
            get
            {
                if (_input.IsPressed(_left)) return -1;
                if (_input.IsPressed(_right)) return 1;
                return 0;
            }
        }
        public bool CanJump => _jumpTimer > 0f;

        public PlatformerInput(Keybind left, Keybind right, Keybind jump, float jumpBuffer)
        {
            _jumpBuffer = jumpBuffer;
            _left = left;
            _right = right;
            _jump = jump;
        }
        public PlatformerInput(float coyoteTime) : this(Keybind.Left, Keybind.Right, Key.Spacebar, coyoteTime) { }

        protected override void OnStart() => _input = Modules.Get<Input>();
        protected override void OnEarlyUpdate(Time time)
        {
            _jumpTimer = Math.Max(0, _jumpTimer - time.DeltaTime);
            if (_input.WasJustPressed(_jump))
                _jumpTimer = _jumpBuffer;
        }
    }
}