using N8Engine.SceneManagement;
using Silk.NET.OpenGL;

namespace N8Engine.Rendering;

public sealed class SpriteRenderer : SceneModule
{
    const string VERTEX_SHADER_SOURCE = @"
    #version 330 core //Using version GLSL version 3.3
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
        FragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);
    }
    ";
    
    readonly float[] _vertices =
    { 
        // X.   Y.    Z.
        0.5f,  0.5f, 0.0f,
        0.5f, -0.5f, 0.0f,
        -0.5f, -0.5f, 0.0f,
        -0.5f,  0.5f, 0.5f
    };
    readonly float[] _indices =
    {
        0, 1, 3,
        1, 2, 3
    };

    GL _gl;
    Debug _debug;
    
    uint _vbo;
    uint _ebo;
    uint _vao;
    uint _shader;

    unsafe void SceneModule.OnSceneLoad(Scene scene)
    {
        _gl = Game.Modules.Get<Graphics>().Get();
        _debug = Game.Modules.Get<Debug>();

        _vao = _gl.GenVertexArray();
        
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
        
        var vertexShader = _gl.CreateShader(ShaderType.VertexShader);
        _gl.ShaderSource(vertexShader, VERTEX_SHADER_SOURCE);
        _gl.CompileShader(vertexShader);
        var infoLog = _gl.GetShaderInfoLog(vertexShader);
        if (!string.IsNullOrWhiteSpace(infoLog))
            _debug.Log($"Error compiling vertex shader {infoLog}");
        
        var fragmentShader = _gl.CreateShader(ShaderType.FragmentShader);
        _gl.ShaderSource(fragmentShader, FRAGMENT_SHADER_SOURCE);
        _gl.CompileShader(fragmentShader);
        infoLog = _gl.GetShaderInfoLog(fragmentShader);
        if (!string.IsNullOrWhiteSpace(infoLog))
            _debug.Log($"Error compiling fragment shader {infoLog}");

        _shader = _gl.CreateProgram();
        _gl.AttachShader(_shader, vertexShader);
        _gl.AttachShader(_shader, fragmentShader);
        _gl.LinkProgram(_shader);
        _gl.GetProgram(_shader, GLEnum.LinkStatus, out var status);
        if (status == 0) _debug.Log($"Error linking shader {_gl.GetProgramInfoLog(_shader)}");
        
        _gl.DetachShader(_shader, vertexShader);
        _gl.DetachShader(_shader, fragmentShader);
        _gl.DeleteShader(vertexShader);
        _gl.DeleteShader(fragmentShader);
    }

    void SceneModule.OnSceneUpdate() { }

    unsafe void SceneModule.OnSceneRender()
    {
        _gl.ClearColor(0, 0, 0, 0);
        _gl.Clear(ClearBufferMask.ColorBufferBit);
        _gl.BindVertexArray(_vao);
        _gl.UseProgram(_shader);
        _gl.DrawElements(PrimitiveType.Triangles, (uint) _indices.Length, DrawElementsType.UnsignedInt, null);
        _gl.BindVertexArray(0);
    }

    void SceneModule.OnSceneUnload()
    {
        _gl.DeleteBuffer(_vbo);
        _gl.DeleteBuffer(_ebo);
        _gl.DeleteVertexArray(_vao);
        _gl.DeleteProgram(_shader);
    }
}