using N8Engine.Inputs;
using N8Engine.Mathematics;
using static N8Engine.Mathematics.Direction;

namespace SampleProject
{
    public sealed class PlayerInputs
    {
        private const float COYOTE_TIME = 0.3f;
        private float _jumpInputTimer;
        
        public Direction CurrentDirection
        {
            get
            {
                if (Key.A.IsPressed() || Key.LeftArrow.IsPressed())
                    return Left;
                if (Key.D.IsPressed() || Key.RightArrow.IsPressed()) 
                    return Right;
                return Direction.None;
            }
        }
        public Direction LastDirectionWhenThereWasInput { get; private set; }
        public bool JustPressedJumpButton => _jumpInputTimer > 0f;

        public void GetInputs(float deltaTime)
        {
            _jumpInputTimer -= deltaTime;
            if (Key.W.WasJustPressed() || Key.UpArrow.WasJustPressed())
                _jumpInputTimer = COYOTE_TIME;

            if (CurrentDirection is Left or Right)
                LastDirectionWhenThereWasInput = CurrentDirection;
        }
    }
}