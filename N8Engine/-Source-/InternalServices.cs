using N8Engine.Internal;
using N8Engine.Rendering;
namespace N8Engine
{
    static class InternalServices
    {
        public static IRenderer Renderer { get; set; }
        public static InternalEvents InternalEvents { get; set; }
        public static UpdateEvents UpdateEvents { get; set; }
        public static RenderingEvents RenderingEvents { get; set; }
    }
}