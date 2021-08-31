using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace N8Engine.Tilemaps
{
    public abstract class TilePalette
    {
        public abstract Sprite Left { get; }
        public abstract Sprite Right { get; }
        public abstract Sprite Top { get; }
        public abstract Sprite Bottom { get; }
        public abstract Sprite TopLeft { get; }
        public abstract Sprite TopRight { get; }
        public abstract Sprite BottomLeft { get; }
        public abstract Sprite BottomRight { get; }
        public abstract Sprite Middle { get; }
        public abstract IntegerVector TileSize { get; }
        public abstract int SortingOrder { get; }
        
        public virtual BaseTilemapObject BaseTilemapObject => GameObject.Create<BaseTilemapObject>("tilemap");
        
        internal Sprite SpriteBasedOnTileType(TileType tileType) => tileType switch
        {
            TileType.Left => Left,
            TileType.Right => Right,
            TileType.Top => Top,
            TileType.Bottom => Bottom,
            TileType.TopLeft => TopLeft,
            TileType.TopRight => TopRight,
            TileType.BottomLeft => BottomLeft,
            TileType.BottomRight => BottomRight,
            TileType.Middle => Middle,
            var _ => Middle
        };
    }
}