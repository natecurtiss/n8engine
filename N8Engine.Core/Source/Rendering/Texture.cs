using System;
using Silk.NET.OpenGL;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace N8Engine.Rendering;

public sealed class Texture : IDisposable
{
    readonly GL _gl;
    readonly uint _handle;

    public unsafe Texture(GL gl, string path)
    {
        _gl = gl;

        var image = Image.Load<Rgba32>(path);
        _handle = _gl.GenTexture();
        Bind();
        _gl.TexImage2D(TextureTarget.Texture2D, 0, (int) InternalFormat.Rgba, (uint) image.Width, (uint) image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, null);
        _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) GLEnum.Repeat);
        _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) GLEnum.Repeat);
        _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) GLEnum.Linear);
        _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) GLEnum.Linear);
        _gl.GenerateMipmap(TextureTarget.Texture2D);
    }
    
    public void Bind(TextureUnit textureSlot = TextureUnit.Texture0)
    {
        _gl.ActiveTexture(textureSlot);
        _gl.BindTexture(TextureTarget.Texture2D, _handle);
    }

    public void Dispose() => _gl.DeleteTexture(_handle);
}