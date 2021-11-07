using System.IO;
using N8Engine;
using N8Engine.Rendering;

namespace SampleProject
{
    public static class SpritesFolder
    {
        static readonly string _path = Services.PathToProject + "\\Sprites";

        public static readonly SpriteSheet Player = new($"{_path}\\player.png", new(16, 16));
    }
}