using System.Collections.Generic;
using N8Engine.Mathematics;

namespace N8Engine.Tilemaps
{
    public abstract class AutoTilemap<TLeft, TRight, TTop, TBottom, TTopRight, TTopLeft, TBottomRight, TBottomLeft, TMiddle>
        where TLeft : GameObject, new()
        where TRight : GameObject, new()
        where TTop : GameObject, new()
        where TBottom : GameObject, new()
        where TTopRight : GameObject, new()
        where TTopLeft : GameObject, new()
        where TBottomLeft : GameObject, new()
        where TBottomRight : GameObject, new()
        where TMiddle : GameObject, new()
    {
        private enum TileType
        {
            Left, Right, Top, Bottom, TopRight, TopLeft, BottomRight, BottomLeft, Middle
        }
        
        public static void Place<T>(Vector position, Vector sizeOfChunkInTiles, Vector tileSize, TilePivot pivot) where T : AutoTilemap
            <TLeft, TRight, TTop, TBottom, TTopRight, TTopLeft, TBottomRight, TBottomLeft, TMiddle>
        {
            if (sizeOfChunkInTiles.X <= 0 || sizeOfChunkInTiles.Y <= 0) return;
            var tiles = new List<GameObject>();
            for (var y = 0; y < (int) sizeOfChunkInTiles.Y; y++)
            {
                for (var x = 0; x < (int) sizeOfChunkInTiles.X; x++)
                {
                    var tileType = GetTileType(new Vector(x, y), sizeOfChunkInTiles);
                    var gameObject = (GameObject) (tileType switch
                    {
                        TileType.Left => GameObject.Create<TLeft>(),
                        TileType.Right => GameObject.Create<TRight>(),
                        TileType.Top => GameObject.Create<TTop>(),
                        TileType.Bottom => GameObject.Create<TBottom>(),
                        TileType.TopRight => GameObject.Create<TTopRight>(),
                        TileType.TopLeft => GameObject.Create<TTopLeft>(),
                        TileType.BottomRight => GameObject.Create<TBottomRight>(),
                        TileType.BottomLeft => GameObject.Create<TBottomLeft>(),
                        TileType.Middle => GameObject.Create<TMiddle>(),
                        var _ => GameObject.Create<TMiddle>()
                    });
                    gameObject.Transform.Position = position + new Vector(x, y) * sizeOfChunkInTiles * tileSize;
                    tiles.Add(gameObject);
                }
            }
            foreach (var tile in tiles)
                tile.Transform.Position = GetPositionFromPivot(pivot, tile.Transform.Position, sizeOfChunkInTiles * tileSize);
        }

        private static TileType GetTileType(Vector tilePosition, Vector numberOfTiles)
        {
            if (numberOfTiles.X == 1)
            {
                if (tilePosition.Y == numberOfTiles.Y - 1) return TileType.Top;
                return TileType.Middle;
            }
            
            if (tilePosition.X == 0)
            {
                if (tilePosition.Y == 0) return TileType.BottomLeft;
                if (tilePosition.Y == numberOfTiles.Y - 1) return TileType.TopLeft;
                return TileType.Left;
            }
            if (tilePosition.X == numberOfTiles.X - 1)
            {
                if (tilePosition.Y == 0) return TileType.BottomRight;
                if (tilePosition.Y == numberOfTiles.Y - 1) return TileType.TopRight;
                return TileType.Right;
            }
            if (tilePosition.Y == 0)
                return TileType.Bottom;
            if (tilePosition.Y == numberOfTiles.Y - 1)
                return TileType.Top;
            
            return TileType.Middle;
        }

        private static Vector GetPositionFromPivot(TilePivot pivot, Vector originalPosition, Vector size) =>
            pivot switch
            {
                TilePivot.Center => originalPosition - size / 2f,
                TilePivot.Top => originalPosition + new Vector(size.X / 2f, size.Y),
                TilePivot.Bottom => originalPosition + new Vector(size.X / 2f, 0f),
                TilePivot.Right => originalPosition + new Vector(size.X, size.Y / 2f),
                TilePivot.Left => originalPosition + new Vector(0f, size.Y / 2f),
                TilePivot.UpperRight => originalPosition + size,
                TilePivot.UpperLeft => originalPosition + new Vector(0f, size.Y),
                TilePivot.BottomRight => originalPosition + new Vector(size.X, 0f),
                TilePivot.BottomLeft => originalPosition,
                var _ => originalPosition
            };
    }
}