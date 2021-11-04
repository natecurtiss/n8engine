using System.Drawing;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    readonly struct ImagePixel : IPixel
    {
        public Color Color { get; }
        public IntVector LocalPosition { get; }

        public ImagePixel(Color color, IntVector localPosition)
        {
            Color = color;
            LocalPosition = localPosition;
        }

        public IPixel With(Color color) => new ImagePixel(color, LocalPosition);
    }
}