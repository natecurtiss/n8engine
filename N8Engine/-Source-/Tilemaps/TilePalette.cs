using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace N8Engine.Tilemaps
{
    public abstract class TilePalette
    {
        public abstract TilemapBase BaseObject { get; }
        public abstract IntegerVector TileSize { get; }
        public abstract int SortingOrder { get; }
        protected abstract Sprite Left { get; }
        protected abstract Sprite Right { get; }
        protected abstract Sprite Top { get; }
        protected abstract Sprite Bottom { get; }
        protected abstract Sprite TopLeft { get; }
        protected abstract Sprite TopRight { get; }
        protected abstract Sprite BottomLeft { get; }
        protected abstract Sprite BottomRight { get; }
        protected abstract Sprite Middle { get; }
        
        internal Vector HalfATile => (Vector) TileSize / 2f;
        
        public Sprite GetSpriteInChunk(IntegerVector localPosition, IntegerVector chunkSizeInTiles)
        {
            const int bottom = 0;
            const int left = 0;
            var top = chunkSizeInTiles.Y - 1;
            var right = chunkSizeInTiles.X - 1;
            var isSingularColumnOfTiles = chunkSizeInTiles.X == 1;

            if (isSingularColumnOfTiles)
            {
                if (localPosition.Y == bottom) return Bottom;
                if (localPosition.Y == top) return Top;                
                return Middle;
            }
            if (localPosition.X == left)
            {
                if (localPosition.Y == bottom) return BottomLeft;
                if (localPosition.Y == top) return TopLeft;
                return Left;
            }
            if (localPosition.X == right)
            {
                if (localPosition.Y == bottom) return BottomRight;
                if (localPosition.Y == top) return TopRight;
                return Right;
            }
            if (localPosition.Y == bottom)
                return Bottom;
            if (localPosition.Y == top)
                return Top;
            
            return Middle;
        }
    }
}