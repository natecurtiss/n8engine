using N8Engine.Mathematics;
using N8Engine.Rendering;
using N8Engine.Tilemaps;
using static SampleProject.AllSprites;

namespace SampleProject
{
    public class GroundPalette : TilePalette
    {
        public override IntegerVector TileSize => new(18, 18);
        public override int SortingOrder => 0;
        protected override Sprite Left => Tilemap.Take(0, 1);
        protected override Sprite Right => Tilemap.Take(2, 1);
        protected override Sprite Top => Tilemap.Take(1, 0);
        protected override Sprite Bottom => Tilemap.Take(1, 2);
        protected override Sprite TopLeft => Tilemap.Take(0, 0);
        protected override Sprite TopRight => Tilemap.Take(2, 0);
        protected override Sprite BottomLeft => Tilemap.Take(0, 2);
        protected override Sprite BottomRight => Tilemap.Take(2, 2);
        protected override Sprite Middle => Tilemap.Take(1, 1);
    }
}