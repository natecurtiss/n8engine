using System;
using System.Drawing;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public class BasicShape : Shape
    {
        protected override Color[] Colors => Array.Empty<Color>();
        protected override IntegerVector Size { get; }
        protected override Vector Offset { get; }
        protected override Pivot Pivot { get; }

        protected BasicShape(IntegerVector size, Vector offset, Pivot pivot)
        {
            Size = size;
            Offset = offset;
            Pivot = pivot;
        }
        
        protected BasicShape(Vector offset, Pivot pivot)
        {
            Offset = offset;
            Pivot = pivot;
        }
    }
}