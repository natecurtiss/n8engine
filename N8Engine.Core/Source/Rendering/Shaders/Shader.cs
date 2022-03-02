using Silk.NET.OpenGL;
using static System.String;
using static Silk.NET.OpenGL.ShaderType;

namespace N8Engine.Rendering;

sealed class Shader
{
    readonly GL _gl;
    readonly string _vertexCode;
    readonly string _fragmentCode;
    uint _programId;
    
    public Shader(GL gl, string vertexCode, string fragmentCode)
    {
        _gl = gl;
        _vertexCode = vertexCode;
        _fragmentCode = fragmentCode;
    }

    public void Load()
    {
        var vertexShader = _gl.CreateShader(VertexShader);
        _gl.ShaderSource(vertexShader, _vertexCode);
        _gl.CompileShader(vertexShader);

        var infoLog = _gl.GetShaderInfoLog(vertexShader);
        if (!IsNullOrWhiteSpace(infoLog))
            Debug.Log($"ERROR COMPILING VERTEX SHADER: {infoLog}");
        
        var fragmentShader = _gl.CreateShader(FragmentShader);
        _gl.ShaderSource(fragmentShader, _fragmentCode);
        _gl.CompileShader(fragmentShader);
        
        infoLog = _gl.GetShaderInfoLog(fragmentShader);
        if (!IsNullOrWhiteSpace(infoLog))
            Debug.Log($"ERROR COMPILING FRAGMENT SHADER: {infoLog}");
        
        _programId = _gl.CreateProgram();
        _gl.AttachShader(_programId, vertexShader);
        _gl.AttachShader(_programId, fragmentShader);
        _gl.LinkProgram(_programId);
        
        _gl.DetachShader(_programId, vertexShader);
        _gl.DetachShader(_programId, fragmentShader);
        _gl.DeleteShader(vertexShader);
        _gl.DeleteShader(fragmentShader);
    }

    public void Use() => _gl.UseProgram(_programId);
}