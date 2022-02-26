using OpenGLTutorials;
using Silk.NET.OpenGL;
using Color = System.Drawing.Color;

new W().Start();

sealed class W : Base
{
    GL _gl = null!;

    protected override void OnLoad()
    {
        base.OnLoad();
        
        // Get the OpenGL API for drawing to the window.
        _gl = GL.GetApi(_window);
    }

    protected override void OnRender(double dt)
    {
        _gl.ClearColor(Color.Ivory);
        _gl.Clear(ClearBufferMask.ColorBufferBit);
    }
}