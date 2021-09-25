using System.Drawing;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public class FilledRectangle : BasicShape
    {
        protected override Color[] Colors { get; }

        public FilledRectangle(Color color, IntegerVector size, Vector offset = default, Pivot pivot = Pivot.Center) : base(size, offset, pivot)
        {
            Colors = new Color[size.X * size.Y]; // TODO does not produce a perfect square - fix later.
            for (var i = 0; i < Colors.Length; i++)
                Colors[i] = color;
        }
    }
}