using N8Engine;
using N8Engine.Inputs;
using N8Engine.Mathematics;

namespace SampleProject
{
    public sealed class PlayerInputs : GameObject
    {
        private const float COYOTE_TIME = 0.3f;
        private float _jumpInputTimer;
        
        public Vector<Direction, Direction> Direction
        {
            get
            {
                var direction = new Vector<Direction, Direction>();
                if (Key.A.IsPressed() || Key.LeftArrow.IsPressed())
                    direction.X = N8Engine.Mathematics.Direction.Left;
                else if (Key.D.IsPressed() || Key.RightArrow.IsPressed()) 
                    direction.X = N8Engine.Mathematics.Direction.Right;
                if (Key.W.IsPressed() || Key.UpArrow.IsPressed()) 
                    direction.Y = N8Engine.Mathematics.Direction.Up;
                else if (Key.S.IsPressed() || Key.DownArrow.IsPressed()) 
                    direction.Y = N8Engine.Mathematics.Direction.Down;
                return direction;
            }
        }
        public bool JustPressedJumpButton => _jumpInputTimer > 0f;

        protected override void OnUpdate(float deltaTime)
        {
            _jumpInputTimer -= deltaTime;
            if (Key.W.WasJustPressed() || Key.UpArrow.WasJustPressed())
                _jumpInputTimer = COYOTE_TIME;
        }
    }
}