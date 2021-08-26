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
        public abstract Vector TileSize { get; }
        
        public virtual GameObject BaseTilemapObject => GameObject.Create<EmptyGameObject>();
    }
}