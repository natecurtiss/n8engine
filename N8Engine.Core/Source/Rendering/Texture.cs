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
        Console.WriteLine(path);
        _gl = gl;
        _handle = _gl.GenTexture();
        Bind();
        _gl.PixelStore(PixelStoreParameter.UnpackAlignment, 1);
        _image = Image.Load<Rgba32>(path);
        _image.ProcessPixelRows(a =>
        {
            fixed (void* d = &MemoryMarshal.GetReference(a.GetRowSpan(0)))
                _gl.TexImage2D(TextureTarget.Texture2D, 0, (int) InternalFormat.Rgba, (uint) _image.Width, (uint) _image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, d);
        });
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