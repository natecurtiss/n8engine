using N8Engine.Mathematics;
using N8Engine.Native;

namespace N8Engine.Inputs
{
    /// <summary>
    /// The input system for the application.
    /// </summary>
    public static class Input
    {
        public static Vector MovementAxis
        {
            get
            {
                var horizontalInput = 0f;
                if (Key.D.IsPressedDown() || Key.RightArrow.IsPressedDown())
                    horizontalInput = 1f;
                else if (Key.A.IsPressedDown() || Key.LeftArrow.IsPressedDown())
                    horizontalInput = -1f;
                
                var verticalInput = 0f;
                if (Key.W.IsPressedDown() || Key.UpArrow.IsPressedDown())
                    verticalInput  = -1f;
                else if (Key.S.IsPressedDown() || Key.DownArrow.IsPressedDown())
                    verticalInput = 1f;

                var axisInput = new Vector(horizontalInput, verticalInput);
                axisInput = axisInput.Normalized;
                return new Vector(axisInput.X, axisInput.Y * 0.5f);
            }
        }
        
        public static bool IsPressedDown(this Key key) => ConsoleInput.GetKeyDown(key);
    }
}