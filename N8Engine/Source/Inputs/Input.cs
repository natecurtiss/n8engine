using N8Engine.Mathematics;
using N8Engine.Native;

namespace N8Engine.Inputs
{
    /// <summary>
    /// The input system for the application.
    /// </summary>
    public static class Input
    {
        public static bool IsDown(this Key key) => ConsoleInput.GetKeyDown(key);

        public static bool IsUp(this Key key) => !key.IsDown();

        public static Vector2 MovementAxis
        {
            get
            {
                float __horizontalInput = 0f;
                if (Key.D.IsDown() || Key.RightArrow.IsDown())
                    __horizontalInput = 1f;
                else if (Key.A.IsDown() || Key.LeftArrow.IsDown())
                    __horizontalInput = -1f;
                
                float __verticalInput = 0f;
                if (Key.W.IsDown() || Key.UpArrow.IsDown())
                    __verticalInput  = 1f;
                else if (Key.S.IsDown() || Key.DownArrow.IsDown())
                    __verticalInput = -1f;

                return new Vector2(__horizontalInput, __verticalInput);
            }
        }
    }
}