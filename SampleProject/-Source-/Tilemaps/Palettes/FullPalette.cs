using N8Engine.Mathematics;
using N8Engine.Rendering;
using N8Engine.Tilemaps;

namespace SampleProject
{
    public class FullPalette : TilePalette
    {
        public override IntegerVector TileSize => new(12, 12);
        public override int SortingOrder => 0;
        protected override Sprite Left => new(SpritesFolder.Path + "wall_left.png");
        protected override Sprite Right => new(SpritesFolder.Path + "wall_right.png");
        protected override Sprite Top => new(SpritesFolder.Path + "wall_top.png");
        protected override Sprite Bottom => new(SpritesFolder.Path + "wall_bottom.png");
        protected override Sprite TopLeft => new(SpritesFolder.Path + "wall_top-left.png");
        protected override Sprite TopRight => new(SpritesFolder.Path + "wall_top-right.png");
        protected override Sprite BottomLeft => new(SpritesFolder.Path + "wall_bottom-left.png");
        protected override Sprite BottomRight => new(SpritesFolder.Path + "wall_bottom-right.png");
        protected override Sprite Middle => null;
    }
}