using System.IO;
using N8Engine;
using N8Engine.Rendering;

namespace SampleProject
{
    public sealed class SpritesFolder
    {
        static readonly string _path = Services.PathToProject + "\\SpritesFolder";

        public static readonly SpriteSheet Player = new($"{_path}\\SpritesFolder", new(16, 16));
    }
}