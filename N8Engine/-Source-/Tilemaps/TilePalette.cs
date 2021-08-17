using N8Engine.Mathematics;

namespace N8Engine.Tilemaps
{
    public abstract class TilePalette
    {
        public abstract GameObject Left { get; }
        public abstract GameObject Right { get; }
        public abstract GameObject Top { get; }
        public abstract GameObject Bottom { get; }
        public abstract GameObject TopLeft { get; }
        public abstract GameObject TopRight { get; }
        public abstract GameObject BottomLeft { get; }
        public abstract GameObject BottomRight { get; }
        public abstract GameObject Middle { get; }
        public abstract Vector TileSize { get; }
        public virtual string Name { get; } = "tilemap";
        public virtual bool ShowDebugCollider { get; } = false;
    }
}