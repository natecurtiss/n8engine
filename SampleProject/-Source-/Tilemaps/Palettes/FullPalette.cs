using N8Engine;
using N8Engine.Mathematics;
using N8Engine.Rendering;
using N8Engine.Tilemaps;

namespace SampleProject
{
    public class FullPalette : TilePalette
    {
        public override GameObject BaseTilemapObject => GameObject.Create<TilemapThatCanBeJumpedOn>();
        public override Sprite Left => new(SpritesFolder.Path + "wall_left.n8sprite");
        public override Sprite Right => new(SpritesFolder.Path + "wall_right.n8sprite");
        public override Sprite Top => new(SpritesFolder.Path + "wall_top.n8sprite");
        public override Sprite Bottom => new(SpritesFolder.Path + "wall_bottom.n8sprite");
        public override Sprite TopLeft => new(SpritesFolder.Path + "wall_top-left.n8sprite");
        public override Sprite TopRight => new(SpritesFolder.Path + "wall_top-right.n8sprite");
        public override Sprite BottomLeft => new(SpritesFolder.Path + "wall_bottom-left.n8sprite");
        public override Sprite BottomRight => new(SpritesFolder.Path + "wall_bottom-right.n8sprite");
        public override Sprite Middle => new(SpritesFolder.Path + "wall_middle.n8sprite");
        public override Vector TileSize => new(12f, 12f);
        public override int SortingOrder => 0;
    }
}