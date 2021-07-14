using N8Engine.Mathematics;
using N8Engine.Native;
using N8Engine.Rendering;

namespace N8Engine
{
    /// <summary>
    /// The input system for the application.
    /// </summary>
    public static class Input
    {
        public static bool IsPressedDown(this Key key) => ConsoleInput.GetKeyDown(key);

        public static Vector MovementAxis
        {
            get
            {
                float __horizontalInput = 0f;
                if (Key.D.IsPressedDown() || Key.RightArrow.IsPressedDown())
                    __horizontalInput = 1f;
                else if (Key.A.IsPressedDown() || Key.LeftArrow.IsPressedDown())
                    __horizontalInput = -1f;
                
                float __verticalInput = 0f;
                if (Key.W.IsPressedDown() || Key.UpArrow.IsPressedDown())
                    __verticalInput  = -1f;
                else if (Key.S.IsPressedDown() || Key.DownArrow.IsPressedDown())
                    __verticalInput = 1f;

                Vector __axisInput = new(__horizontalInput, __verticalInput);
                __axisInput = __axisInput.Normalized;
                return new Vector(__axisInput.X, __axisInput.Y * 0.5f);
            }
        }
    }
}