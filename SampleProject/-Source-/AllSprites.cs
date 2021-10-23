using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace SampleProject
{
    public static class AllSprites
    {
        public static string PathToFolder => System.IO.Path.GetFullPath("../../../../") + "SampleProject\\Sprites\\";
        public static readonly SpriteSheet Player = new(PathToFolder + "entity_player", IntegerVector.One * 18);
    }
}