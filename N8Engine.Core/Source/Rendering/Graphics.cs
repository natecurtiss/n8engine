using Silk.NET.OpenGL;

namespace N8Engine.Rendering;

sealed class Graphics : GameModule
{
    public GL Lib { get; }
    public Graphics(GL gl) => Lib = gl;
}