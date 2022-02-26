using OpenGLTutorials;
using Silk.NET.OpenGL;

new W().Start();

sealed class W : Base
{
    GL _gl = null!;
    uint _vertexBufferObject;
    uint _vertexArrayObject;

    
    protected override void OnLoad()
    {
        base.OnLoad();
        // Get the OpenGL API for drawing to the window.
        _gl = GL.GetApi(_window);
        
        // Create the vertex array object (array of VBOs).
        _vertexArrayObject = _gl.GenVertexArray();
        _gl.BindVertexArray(_vertexArrayObject);
        
        // Create the vertex buffer object that holds the vertex data.
        _vertexBufferObject = _gl.GenBuffer();
        _gl.BindBuffer(BufferTargetARB.ArrayBuffer, _vertexBufferObject);
    }
}