using N8Engine;
using N8Engine.Inputs;
using N8Engine.Mathematics;

namespace SampleProject
{
    public sealed class PlayerInputs : GameObject
    {
        private const float COYOTE_TIME = 0.3f;
        private float _jumpInputTimer;
        
        public Vector Axis
        {
            get
            {
                var axisInput = new Vector();
                if (Key.A.IsPressed() || Key.LeftArrow.IsPressed()) 
                    axisInput.X = -1f;
                else if (Key.D.IsPressed() || Key.RightArrow.IsPressed()) 
                    axisInput.X = 1f;
                if (Key.W.IsPressed() || Key.UpArrow.IsPressed()) 
                    axisInput.Y = 1f;
                else if (Key.S.IsPressed() || Key.DownArrow.IsPressed()) 
                    axisInput.Y = -1f;
                return axisInput;
            }
        }
        public bool JustPressedJump => _jumpInputTimer > 0f;

        protected override void OnUpdate(float deltaTime)
        {
            _jumpInputTimer -= deltaTime;
            if (Key.W.WasJustPressed() || Key.UpArrow.WasJustPressed())
                _jumpInputTimer = COYOTE_TIME;
        }
    }
}