using System;
using System.IO;
using System.Numerics;
using Silk.NET.OpenGL;
using static System.String;
using static Silk.NET.OpenGL.ShaderType;
using static N8Engine.Windowing.Window;

namespace N8Engine.Rendering;

sealed class Shader
{
    readonly Debug _debug;
    readonly string _vertex;
    readonly string _fragment;
    
    uint _handle;
    
    public Shader(string vertex, string fragment)
    {
        _vertex = File.ReadAllText(vertex);
        _fragment = File.ReadAllText(fragment);
        _debug = Game.Modules.Get<Debug>();
    }

    public void Load()
    {
        var vertexShader = Graphics.CreateShader(VertexShader);
        Graphics.ShaderSource(vertexShader, _vertex);
        Graphics.CompileShader(vertexShader);

        var infoLog = Graphics.GetShaderInfoLog(vertexShader);
        if (!IsNullOrWhiteSpace(infoLog))
            _debug.Log($"ERROR COMPILING VERTEX SHADER: {infoLog}");
        
        var fragmentShader = Graphics.CreateShader(FragmentShader);
        Graphics.ShaderSource(fragmentShader, _fragment);
        Graphics.CompileShader(fragmentShader);
        
        infoLog = Graphics.GetShaderInfoLog(fragmentShader);
        if (!IsNullOrWhiteSpace(infoLog))
            _debug.Log($"ERROR COMPILING FRAGMENT SHADER: {infoLog}");
        
        _handle = Graphics.CreateProgram();
        Graphics.AttachShader(_handle, vertexShader);
        Graphics.AttachShader(_handle, fragmentShader);
        Graphics.LinkProgram(_handle);
        
        Graphics.DetachShader(_handle, vertexShader);
        Graphics.DetachShader(_handle, fragmentShader);
        Graphics.DeleteShader(vertexShader);
        Graphics.DeleteShader(fragmentShader);
    }

    public void Use() => Graphics.UseProgram(_handle);
    
    public unsafe void SetMatrix(string name, Matrix4x4 value)
    {
        var location = Graphics.GetUniformLocation(_handle, name);
        Graphics.UniformMatrix4(location, 1, false, (float*) &value);
    }
    
    public void SetUniform(string name, int value)
    {
        var location = Graphics.GetUniformLocation(_handle, name);
        if (location == -1)
            throw new InvalidOperationException($"{name} uniform not found on shader.");
        Graphics.Uniform1(location, value);
    }

    public void SetUniform(string name, float value)
    {
        var location = Graphics.GetUniformLocation(_handle, name);
        if (location == -1)
            throw new InvalidOperationException($"{name} uniform not found on shader.");
        Graphics.Uniform1(location, value);
    }

    public void SetUniform(int location, int value) => Graphics.Uniform1(location, value);
    public void SetUniform(int location, float value) => Graphics.Uniform1(location, value);
}