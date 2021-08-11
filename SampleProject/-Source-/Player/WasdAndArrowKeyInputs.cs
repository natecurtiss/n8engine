using N8Engine.Inputs;
using N8Engine.Mathematics;

namespace SampleProject
{
    public sealed class WasdAndArrowKeyInputs
    {
        public Vector InputVector
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
    }
}