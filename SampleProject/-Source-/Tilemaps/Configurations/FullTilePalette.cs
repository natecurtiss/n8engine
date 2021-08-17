using N8Engine;
using N8Engine.Mathematics;
using N8Engine.Tilemaps;

namespace SampleProject
{
    public class FullTilePalette : TilePalette
    {
        public override GameObject Left => GameObject.Create<LeftWall>();
        public override GameObject Right => GameObject.Create<RightWall>();
        public override GameObject Top => GameObject.Create<TopWall>();
        public override GameObject Bottom => GameObject.Create<BottomWall>();
        public override GameObject TopLeft => GameObject.Create<TopLeftWall>();
        public override GameObject TopRight => GameObject.Create<TopRightWall>();
        public override GameObject BottomLeft => GameObject.Create<BottomLeftWall>();
        public override GameObject BottomRight => GameObject.Create<BottomRightWall>();
        public override GameObject Middle => GameObject.Create<EmptyGameObject>();
        public override Vector TileSize => new(12f, 12f);
    }
}