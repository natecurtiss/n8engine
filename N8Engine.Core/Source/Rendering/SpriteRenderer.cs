using System.Numerics;
using Silk.NET.OpenGL;

namespace N8Engine.Rendering;

sealed class SpriteRenderer
{
    readonly Shader _shader;
    readonly GL _gl;

    public unsafe SpriteRenderer(GL gl)
    {
        _gl = gl;
        
        _shader = new(_gl, "../../../../Assets/Shaders/sprite_vertex.glsl", "../../../../Assets/Shaders/sprite_fragment.glsl");
        _shader.Load();
        
        var vao = _gl.GenVertexArray();
        var vbo = _gl.GenBuffer();
        
        var vertices = new[]
        {
            // Pos.     // Tex.
            0.0f, 1.0f, 0.0f, 1.0f,
            1.0f, 0.0f, 1.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f, 
    
            0.0f, 1.0f, 0.0f, 1.0f,
            1.0f, 1.0f, 1.0f, 1.0f,
            1.0f, 0.0f, 1.0f, 0.0f
        };
        
        _gl.BindBuffer(GLEnum.ArrayBuffer, vbo);
        fixed (void* first = &vertices[0])
        {
            _gl.BufferData(GLEnum.ArrayBuffer, (nuint) (sizeof(nuint) * vertices.Length), first, GLEnum.StaticDraw);
        }
        
        _gl.BindVertexArray(vao);
        _gl.EnableVertexAttribArray(0);
        _gl.VertexAttribPointer(0, 4, GLEnum.Float, Boolean.False, 4 * sizeof(float), null);
        
        _gl.BindBuffer(GLEnum.ArrayBuffer, 0);
        _gl.BindVertexArray(0);

    }
    
    public void Draw()
    {
        _shader.Use();
        var texture = _gl.GenTexture();
        // TODO: Make texture a uint.
    }
}