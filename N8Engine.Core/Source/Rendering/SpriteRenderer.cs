using N8Engine.SceneManagement;
using Silk.NET.OpenGL;

namespace N8Engine.Rendering;

public sealed class SpriteRenderer : SceneModule
{
    const string VERTEX_SHADER_SOURCE = @"
    #version 330 core

    layout (location = 0) in vec3 vPos;
    layout (location = 1) in vec4 vColor;

    uniform float uBlue;

    out vec4 fColor;

    void main()
    {
        gl_Position = vec4(vPos, 1.0);
        fColor = vColor;
    }
    ";
    const string FRAGMENT_SHADER_SOURCE = @"
    #version 330 core

    in vec4 fColor;
    out vec4 FragColor;

    void main()
    {
        FragColor = fColor;
    }
    ";
    
    readonly float[] _vertices =
    { 
        // X     Y     Z    R  G  B  A
         0.5f,  0.5f, 0.0f, 1f, 0f, 0f, 1f,
         0.5f, -0.5f, 0.0f, 0f, 0f, 1f, 1f,
        -0.5f, -0.5f, 0.0f, 0f, 1f, 0f, 1f,
        -0.5f,  0.5f, 0.0f, 1f, 1f, 0f, 1f
    };
    readonly uint[] _indices =
    {
        0, 1, 3,
        1, 2, 3
    };

    GL _gl;

    BufferObject<float> _vbo;
    BufferObject<uint> _ebo;
    VertexArrayObject<float, uint> _vao;
    Shader _shader;

    unsafe void SceneModule.OnSceneLoad(Scene scene)
    {
        _gl = Game.Modules.Get<Graphics>().Get();
        
        _vbo = new(_gl, _vertices, BufferTargetARB.ArrayBuffer);
        _ebo = new(_gl, _indices, BufferTargetARB.ElementArrayBuffer);
        _vao = new(_gl, _vbo, _ebo);

        _vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 7, 0);
        _vao.VertexAttributePointer(1, 4, VertexAttribPointerType.Float, 7, 3);

        _shader = new(_gl, VERTEX_SHADER_SOURCE, FRAGMENT_SHADER_SOURCE);
    }

    void SceneModule.OnSceneUpdate() { }

    unsafe void SceneModule.OnSceneRender()
    {
        _gl.ClearColor(0, 0, 0, 0);
        _gl.Clear(ClearBufferMask.ColorBufferBit);
        _vao.Bind();
        _shader.Use();
        _gl.DrawElements(PrimitiveType.Triangles, (uint) _indices.Length, DrawElementsType.UnsignedInt, null);
    }

    void SceneModule.OnSceneUnload()
    {
        _vbo.Dispose();
        _ebo.Dispose();
        _vao.Dispose();
        _shader.Dispose();
    }
}