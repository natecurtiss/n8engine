using N8Engine.Mathematics;

namespace N8Engine.InputSystem
{
    public sealed class TopDownInput : Component
    {
        readonly Keybind _left;
        readonly Keybind _right;
        readonly Keybind _down;
        readonly Keybind _up;

        Input _input;

        public IntVector Axis => new(Horizontal, Vertical);
        public int Horizontal
        {
            get
            {
                if (_input.IsPressed(_left)) return -1;
                if (_input.IsPressed(_right)) return 1;
                return 0;
            }
        }
        public int Vertical
        {
            get
            {
                if (_input.IsPressed(_down)) return -1;
                if (_input.IsPressed(_up)) return 1;
                return 0;
            }
        }

        public TopDownInput(Keybind left, Keybind right, Keybind down, Keybind up)
        {
            _left = left;
            _right = right;
            _down = down;
            _up = up;
        }
        public TopDownInput() : this(Keybind.Left, Keybind.Right, Keybind.Down, Keybind.Up) { }
        
        protected override void OnStart() => _input = Modules.Get<Input>();
    }
}