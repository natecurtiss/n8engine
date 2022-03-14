using System;
using System.Runtime.InteropServices;
using Silk.NET.OpenGL;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace N8Engine.Rendering;

public sealed class Texture : IDisposable
{
    readonly GL _gl;
    readonly uint _handle;
    readonly Image<Rgba32> _image;

    public unsafe Texture(GL gl, string path)
    {
        _gl = gl;
        _handle = _gl.GenTexture();
        Bind();
        
        _image = Image.Load<Rgba32>(path);
        var pixels = new Rgba32[_image.Width * _image.Height];
        for (var y = 0; y < _image.Height; y++)
        {
            for (var x = 0; x < _image.Width; x++)
            {
                pixels[y * _image.Width + x] = _image[x, y];
            }
        }
        fixed (void* data = &pixels[0])
            _gl.TexImage2D(TextureTarget.Texture2D, 0, (int) InternalFormat.Rgba, (uint) _image.Width, (uint) _image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, data);
        
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

    public void Dispose()
    {
        _image.Dispose();
        _gl.DeleteTexture(_handle);
    }
}