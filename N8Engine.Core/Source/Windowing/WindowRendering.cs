using Silk.NET.OpenGL;

namespace N8Engine.Windowing;

// Maybe change accessibility of this in the future? (public vs. internal)
public interface WindowRendering
{
    GL GL { get; }
}