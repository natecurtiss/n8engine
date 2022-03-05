using System.Numerics;
using Silk.NET.OpenGL;
using static System.String;
using static Silk.NET.OpenGL.ShaderType;

namespace N8Engine.Rendering;

sealed class Shader
{
    readonly Debug _debug;
    readonly GL _gl;
    readonly string _vertex;
    readonly string _fragment;
    
    uint _programId;
    
    public Shader(GL gl, string vertex, string fragment)
    {
        _gl = gl;
        _vertex = vertex;
        _fragment = fragment;
        _debug = Game.Modules.Get<Debug>();
    }

    public void Load()
    {
        var vertexShader = _gl.CreateShader(VertexShader);
        _gl.ShaderSource(vertexShader, _vertex);
        _gl.CompileShader(vertexShader);

        var infoLog = _gl.GetShaderInfoLog(vertexShader);
        if (!IsNullOrWhiteSpace(infoLog))
            _debug.Log($"ERROR COMPILING VERTEX SHADER: {infoLog}");
        
        var fragmentShader = _gl.CreateShader(FragmentShader);
        _gl.ShaderSource(fragmentShader, _fragment);
        _gl.CompileShader(fragmentShader);
        
        infoLog = _gl.GetShaderInfoLog(fragmentShader);
        if (!IsNullOrWhiteSpace(infoLog))
            _debug.Log($"ERROR COMPILING FRAGMENT SHADER: {infoLog}");
        
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
    
    public unsafe void SetMatrix(string name, Matrix4x4 value)
    {
        var location = _gl.GetUniformLocation(_programId, name);
        _gl.UniformMatrix4(location, 1, false, (float*) &value);
    }
}