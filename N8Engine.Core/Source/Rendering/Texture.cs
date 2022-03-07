using System.Runtime.InteropServices;
using Silk.NET.OpenGL;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using static Silk.NET.OpenGL.TextureTarget;
using static N8Engine.Windowing.Window;

namespace N8Engine.Rendering;

sealed class Texture
{
    uint _handle;

    public unsafe Texture(string path)
    {
        var image = Image.Load<Rgba32>(path);

        image.ProcessPixelRows(accessor =>
        {
            fixed(void* data = &MemoryMarshal.GetReference(accessor.GetRowSpan(0)))
            {
                _handle = Graphics.GenTexture();
                Bind();
                Graphics.TexImage2D(Texture2D, 0, (int) InternalFormat.Rgba, (uint) image.Width, (uint) image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, data);
                Graphics.TexParameter(Texture2D, TextureParameterName.TextureWrapS, (int) GLEnum.Repeat);
                Graphics.TexParameter(Texture2D, TextureParameterName.TextureWrapT, (int) GLEnum.Repeat);
                Graphics.TexParameter(Texture2D, TextureParameterName.TextureMinFilter, (int) GLEnum.Linear);
                Graphics.TexParameter(Texture2D, TextureParameterName.TextureMagFilter, (int) GLEnum.Linear);
                Graphics.GenerateMipmap(Texture2D);
            }
        });
    }

    public void Bind(TextureUnit textureSlot = TextureUnit.Texture0)
    {
        Graphics.ActiveTexture(textureSlot);
        Graphics.BindTexture(Texture2D, _handle);
    }
}