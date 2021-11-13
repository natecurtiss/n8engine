using System.IO;
using System.Runtime.CompilerServices;
using N8Engine;
using static N8Engine.Sprite;

namespace SampleProject
{
    static class Sprites
    {
        // Unfortunately there's not really a good way for me to integrate this into the engine
        // so just copy-paste this into your project lol.
#nullable enable
        static string SpritesFolder => _pathToFolder ??= GetPathToFolder();
        static string? _pathToFolder;
        static string GetPathToFolder([CallerFilePath] string path = "") => Path.Combine($"{path[..^$"{nameof(Sprites)}.cs".Length]}", @"..\") + "Sprites\\";
#nullable disable

        public static readonly Sprite
            Player = FromImage(SpritesFolder + "player.png");
    }
}