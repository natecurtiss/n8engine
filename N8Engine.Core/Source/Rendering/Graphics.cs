using Silk.NET.OpenGL;

namespace N8Engine.Rendering;

sealed class Graphics : Module
{
    public static implicit operator Graphics(GL gl) => new(gl);
    public static implicit operator GL(Graphics g) => g.Lib;
    
    public GL Lib { get; }
    public Graphics(GL gl) => Lib = gl;
}