using Silk.NET.OpenGL;

namespace N8Engine.Rendering;

sealed class Graphics : GameModule
{
    public static implicit operator Graphics(GL gl) => new(gl);
    readonly GL _gl;
    Graphics(GL gl) => _gl = gl;
    public GL Get() => _gl;
}