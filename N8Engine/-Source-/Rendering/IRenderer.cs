using System.Drawing;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public interface IRenderer
    {
        void Render(IRenderable renderable, IntVector objectPosition);
        internal void ChangeBackground(Color color);
        internal void DisplayPixels();
    }
}