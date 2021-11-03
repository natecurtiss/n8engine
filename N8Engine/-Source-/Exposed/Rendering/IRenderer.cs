using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    interface IRenderer
    {
        void Render(IRenderable renderable, IntVector objectPosition);
    }
}