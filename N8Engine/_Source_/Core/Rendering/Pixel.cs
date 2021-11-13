using System.Drawing;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public readonly struct Pixel
    {
        public readonly IntVector LocalPosition;
        public readonly Color Color;

        public Pixel(IntVector localPosition, Color color)
        {
            LocalPosition = localPosition;
            Color = color;
        }

        internal RenderedPixel AsRendered(int sortingOrder) => new(Color, sortingOrder);
    }
}