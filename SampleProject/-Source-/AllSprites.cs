using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace SampleProject
{
    public static class AllSprites
    {
        public static string PathToFolder => System.IO.Path.GetFullPath("../../../../") + "SampleProject\\Sprites\\";
        public static readonly SpriteSheet Player = new(PathToFolder + "entity_player.png", IntegerVector.One * 18, IntegerVector.Up * 5);
        public static readonly SpriteSheet Tilemap = new(PathToFolder + "tilemap.png", IntegerVector.One * 18);
    }
}