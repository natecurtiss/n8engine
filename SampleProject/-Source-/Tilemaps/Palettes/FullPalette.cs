using N8Engine.Mathematics;
using N8Engine.Rendering;
using N8Engine.Tilemaps;

namespace SampleProject
{
    public class FullPalette : TilePalette
    {
        public override IntegerVector TileSize => new(12, 12);
        public override int SortingOrder => 0;
        protected override Sprite Left => new(AllSprites.PathToFolder + "wall_left.png");
        protected override Sprite Right => new(AllSprites.PathToFolder + "wall_right.png");
        protected override Sprite Top => new(AllSprites.PathToFolder + "wall_top.png");
        protected override Sprite Bottom => new(AllSprites.PathToFolder + "wall_bottom.png");
        protected override Sprite TopLeft => new(AllSprites.PathToFolder + "wall_top-left.png");
        protected override Sprite TopRight => new(AllSprites.PathToFolder + "wall_top-right.png");
        protected override Sprite BottomLeft => new(AllSprites.PathToFolder + "wall_bottom-left.png");
        protected override Sprite BottomRight => new(AllSprites.PathToFolder + "wall_bottom-right.png");
        protected override Sprite Middle => null;
    }
}