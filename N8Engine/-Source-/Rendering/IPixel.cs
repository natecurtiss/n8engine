using System.Drawing;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    interface IPixel
    {
        Color Color { get; }
        IntVector LocalPosition { get; }

        public IPixel With(Color color);
    }
}