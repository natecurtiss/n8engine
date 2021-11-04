using System.Drawing;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    interface IRenderer
    {
        void Render(IRenderable renderable, IntVector objectPosition);
        void ChangeBackground(Color color);
        internal void DisplayPixels();
    }
}