using System;
using System.Numerics;
using Silk.NET.OpenGL;
using Silk.NET.SDL;
using static N8Engine.Windowing.Window;
using Boolean = Silk.NET.OpenGL.Boolean;

namespace N8Engine.Rendering;

// For now this only supports Sprites but this might change.
sealed class Renderer
{
    readonly Shader _shader;
    Texture _texture;

    uint _vao;
    uint _vbo;

    public unsafe Renderer()
    {
        _shader = new("../../../../Assets/Shaders/sprite.vert", "../../../../Assets/Shaders/sprite.frag");
        _shader.Load();
        
        _vao = Graphics.GenVertexArray();
        _vbo = Graphics.GenBuffer();
        
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
        
        Graphics.BindBuffer(GLEnum.ArrayBuffer, _vbo);
        fixed (void* first = &vertices[0])
        {
            Graphics.BufferData(GLEnum.ArrayBuffer, (nuint) (sizeof(nuint) * vertices.Length), first, GLEnum.StaticDraw);
        }
        
        Graphics.BindVertexArray(_vao);
        Graphics.EnableVertexAttribArray(0);
        Graphics.VertexAttribPointer(0, 4, GLEnum.Float, Boolean.False, 4 * sizeof(float), null);
        
        Graphics.BindBuffer(GLEnum.ArrayBuffer, 0);
        Graphics.BindVertexArray(0);

    }
    
    public void Draw(Renderable renderable)
    {
        _texture = renderable.Texture;
        Graphics.Clear((uint) ClearBufferMask.ColorBufferBit);

        var translation = Matrix4x4.CreateTranslation(renderable.Position.X, renderable.Position.Y, 0f);
        var scale = Matrix4x4.CreateScale(renderable.Scale.X, renderable.Scale.Y, 1f);
        var rotation = renderable.Rotation * (MathF.PI / 180);
        
        _texture.Bind();
        _shader.SetMatrix("model", scale * rotation * translation);
        _shader.SetMatrix("projection", renderable.Camera.Projection());
        _shader.SetUniform("image", 0);
        _shader.Use();

        Graphics.BindVertexArray(_vao);
        Graphics.DrawArrays(PrimitiveType.Triangles, 0, 6);
        Graphics.BindVertexArray(0);
    }
}