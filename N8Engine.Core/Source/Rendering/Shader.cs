using System;
using Silk.NET.OpenGL;

namespace N8Engine.Rendering;

sealed class Shader : IDisposable
{
    readonly GL _gl;
    readonly uint _shader;

    public Shader(GL gl, string vertexSource, string fragmentSource)
    {
        _gl = gl;
        var vertex = Load(ShaderType.VertexShader, vertexSource);
        var fragment = Load(ShaderType.FragmentShader, fragmentSource);
        _shader = _gl.CreateProgram();
        _gl.AttachShader(_shader, vertex);
        _gl.AttachShader(_shader, fragment);
        _gl.LinkProgram(_shader);
        _gl.GetProgram(_shader, GLEnum.LinkStatus, out var status);
        if (status == 0)
            throw new ShaderProgramFailedLinkException($"Program failed to link with error: {_gl.GetProgramInfoLog(_shader)}");
        _gl.DetachShader(_shader, vertex);
        _gl.DetachShader(_shader, fragment);
        _gl.DeleteShader(vertex);
        _gl.DeleteShader(fragment);
    }

    public void Use() => _gl.UseProgram(_shader);
    
    public void Dispose() => _gl.DeleteProgram(_shader);

    public void SetUniform(string name, int value)
    {
        var location = _gl.GetUniformLocation(_shader, name);
        if (location == -1)
            throw new MissingUniformOnShaderException($"{name} uniform not found on shader.");
        _gl.Uniform1(location, value);
    }

    public void SetUniform(string name, float value)
    {
        var location = _gl.GetUniformLocation(_shader, name);
        if (location == -1)
            throw new MissingUniformOnShaderException($"{name} uniform not found on shader.");
        _gl.Uniform1(location, value);
    }

    uint Load(ShaderType type, string source)
    {
        var loaded = _gl.CreateShader(type);
        _gl.ShaderSource(loaded, source);
        _gl.CompileShader(loaded);
        var infoLog = _gl.GetShaderInfoLog(loaded);
        if (!string.IsNullOrWhiteSpace(infoLog))
            throw new ShaderFailedCompilationException($"Error compiling shader of type {type}, failed with error {infoLog}");
        return loaded;
    }
}