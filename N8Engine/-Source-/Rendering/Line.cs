using System.Drawing;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public sealed class Line : Shape
    {
        protected override Color[] Colors { get; }
        protected override IntegerVector Size { get; }
        protected override Pivot Pivot { get; }
        protected override Vector Offset { get; }

        public Line(Color color, int length, Vector offset = default, Pivot pivot = Pivot.Center)
        {
            Colors = new Color[length];
            for (var i = 0; i < Colors.Length; i++)
                Colors[i] = color;
            Size = new IntegerVector(length, 1);
            Pivot = pivot;
            Offset = offset;
        }
    }
}