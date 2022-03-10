using N8Engine.SceneManagement;
using Silk.NET.OpenGL;

namespace N8Engine.Rendering;

public sealed class SpriteRenderer : SceneModule
{
    const string VERTEX_SHADER_SOURCE = @"
    #version 330 core
    layout (location = 0) in vec4 vPos;
    
    void main()
    {
        gl_Position = vec4(vPos.x, vPos.y, vPos.z, 1.0);
    }
    ";
    const string FRAGMENT_SHADER_SOURCE = @"
    #version 330 core
    layout (location = 0) out vec4 FragColor;
    void main()
    {
        FragColor = vec4(1.0f, 1.0f, 1.0f, 1.0f);
    }
    ";
    
    readonly float[] _vertices =
    { 
        // X.   Y.    Z.
        0.5f,  0.5f, 0.0f,
        0.5f, -0.5f, 0.0f,
        -0.5f, -0.5f, 0.0f,
        -0.5f,  0.5f, 0.0f
    };
    readonly uint[] _indices =
    {
        0, 1, 3,
        1, 2, 3
    };

    GL _gl;

    uint _vbo;
    uint _ebo;
    uint _vao;
    Shader _shader;

    unsafe void SceneModule.OnSceneLoad(Scene scene)
    {
        _gl = Game.Modules.Get<Graphics>().Get();

        _vao = _gl.GenVertexArray();
        _gl.BindVertexArray(_vao);

        _vbo = _gl.GenBuffer();
        _gl.BindBuffer(BufferTargetARB.ArrayBuffer, _vbo);
        fixed (void* v = &_vertices[0])
        {
            _gl.BufferData(GLEnum.ArrayBuffer, (nuint) (sizeof(uint) * _vertices.Length), v, GLEnum.StaticDraw);
        }

        _ebo = _gl.GenBuffer();
        _gl.BindBuffer(GLEnum.ElementArrayBuffer, _ebo);
        fixed (void* i = &_indices[0])
        {
            _gl.BufferData(BufferTargetARB.ElementArrayBuffer, (nuint) (sizeof(uint) * _indices.Length), i, BufferUsageARB.StaticDraw);
        }

        _gl.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), null);
        _gl.EnableVertexAttribArray(0);
        _gl.BindVertexArray(0);
        _gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0);

        _shader = new(_gl, VERTEX_SHADER_SOURCE, FRAGMENT_SHADER_SOURCE);
    }

    void SceneModule.OnSceneUpdate() { }

    unsafe void SceneModule.OnSceneRender()
    {
        _gl.ClearColor(0, 0, 0, 0);
        _gl.Clear(ClearBufferMask.ColorBufferBit);
        _gl.BindVertexArray(_vao);
        _shader.Use();
        _gl.DrawElements(PrimitiveType.Triangles, (uint) _indices.Length, DrawElementsType.UnsignedInt, null);
        _gl.BindVertexArray(0);
    }

    void SceneModule.OnSceneUnload()
    {
        _gl.DeleteBuffer(_vbo);
        _gl.DeleteBuffer(_ebo);
        _gl.DeleteVertexArray(_vao);
        _shader.Dispose();
    }
}