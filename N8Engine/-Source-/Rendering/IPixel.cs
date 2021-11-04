using System.Drawing;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public interface IPixel
    {
        Color Color { get; }
        IntVector LocalPosition { get; }

        IPixel With(Color color);
    }
}