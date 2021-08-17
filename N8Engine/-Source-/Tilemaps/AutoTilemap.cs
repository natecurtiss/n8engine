using System.Collections.Generic;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace N8Engine.Tilemaps
{
    public static class AutoTilemap<TLeft, TRight, TTop, TBottom, TTopRight, TTopLeft, TBottomRight, TBottomLeft, TMiddle>
        where TLeft : GameObject, new()
        where TRight : GameObject, new()
        where TTop : GameObject, new()
        where TBottom : GameObject, new()
        where TTopRight : GameObject, new()
        where TTopLeft : GameObject, new()
        where TBottomRight : GameObject, new()
        where TBottomLeft : GameObject, new()
        where TMiddle : GameObject, new()
    {
        private enum TileType
        {
            Left, Right, Top, Bottom, TopRight, TopLeft, BottomRight, BottomLeft, Middle
        }
        
        public static void Place(Vector position, Vector sizeOfChunkInTiles, Vector tileSize, TilePivot pivot, bool showDebugCollider = false, string name = "tilemap")
        {
            if (sizeOfChunkInTiles.X <= 0 || sizeOfChunkInTiles.Y <= 0) return;

            var actualTileSize = new Vector(tileSize.X * Window.RATIO_OF_HORIZONTAL_PIXELS_TO_VERTICAL_PIXELS, tileSize.Y);
            var tiles = new List<GameObject>();
            for (var y = 0; y < (int) sizeOfChunkInTiles.Y; y++)
            {
                for (var x = 0; x < (int) sizeOfChunkInTiles.X; x++)
                {
                    var tileType = GetTileType(new Vector(x, y), sizeOfChunkInTiles);
                    var gameObject = (GameObject) (tileType switch
                    {
                        TileType.Left => GameObject.Create<TLeft>($"{name}_left"),
                        TileType.Right => GameObject.Create<TRight>($"{name}_right"),
                        TileType.Top => GameObject.Create<TTop>($"{name}_top"),
                        TileType.Bottom => GameObject.Create<TBottom>($"{name}_bottom"),
                        TileType.TopRight => GameObject.Create<TTopRight>($"{name}_top-right"),
                        TileType.TopLeft => GameObject.Create<TTopLeft>($"{name}_top-left"),
                        TileType.BottomRight => GameObject.Create<TBottomRight>($"{name}_bottom-right"),
                        TileType.BottomLeft => GameObject.Create<TBottomLeft>($"{name}_bottom-left"),
                        TileType.Middle => GameObject.Create<TMiddle>($"{name}_middle"),
                        var _ => GameObject.Create<TMiddle>($"{name}_middle")
                    });
                    gameObject.Transform.Position = position + new Vector(x, y) * actualTileSize;
                    tiles.Add(gameObject);
                }
            }
            var chunkSize = sizeOfChunkInTiles * actualTileSize;
            foreach (var tile in tiles)
                tile.Transform.Position = GetTilePositionFromPivot(pivot, tile.Transform.Position, chunkSize, actualTileSize);
            
            var tilemapCollider = GameObject.Create<EmptyGameObject>(name);
            tilemapCollider.Transform.Position = GetColliderPositionFromPivot(position, pivot, chunkSize);
            tilemapCollider.Collider.Size = sizeOfChunkInTiles * tileSize;
            tilemapCollider.Collider.ShowDebugCollider = showDebugCollider;
        }

        private static TileType GetTileType(Vector tilePosition, Vector numberOfTiles)
        {
            if (numberOfTiles.X == 1)
            {
                if (tilePosition.Y == numberOfTiles.Y - 1) return TileType.Top;                
                if (tilePosition.Y == 0) return TileType.Bottom;
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

        private static Vector GetTilePositionFromPivot(TilePivot pivot, Vector position, Vector chunkSize, Vector actualTileSize)
        {
            var newPosition = pivot switch
            {
                TilePivot.Center => position - chunkSize / 2f,
                TilePivot.Top => position - chunkSize / 2f + new Vector(0f, chunkSize.Y / 2f),
                TilePivot.Bottom => position - chunkSize / 2f + new Vector(0f, -chunkSize.Y / 2f),
                TilePivot.Right => position + new Vector(-chunkSize.X, -chunkSize.Y / 2f),
                TilePivot.Left => position + new Vector(0f, -chunkSize.Y / 2f),
                TilePivot.TopRight => position + new Vector(-chunkSize.X, 0f),
                TilePivot.TopLeft => position,
                TilePivot.BottomRight => position - chunkSize,
                TilePivot.BottomLeft => position + new Vector(0f, -chunkSize.Y),
                var _ => position
            };
            newPosition += actualTileSize / 2f;
            return newPosition;
        }

        private static Vector GetColliderPositionFromPivot(Vector position, TilePivot pivot, Vector chunkSize)
        {
            var positionWithCenterPivot = pivot switch
            {
                TilePivot.Center => position,
                TilePivot.Top => position + new Vector(0f, chunkSize.Y / 2f),
                TilePivot.Bottom => position + new Vector(0f, -chunkSize.Y / 2f),
                TilePivot.Right => position + new Vector(-chunkSize.X / 2f, 0f),
                TilePivot.Left => position + new Vector(chunkSize.X / 2f, 0f),
                TilePivot.TopRight => position + new Vector(-chunkSize.X, chunkSize.Y) / 2f,
                TilePivot.TopLeft => position + new Vector(chunkSize.X, chunkSize.Y) / 2f,
                TilePivot.BottomRight => position + new Vector(-chunkSize.X, -chunkSize.Y) / 2f,
                TilePivot.BottomLeft => position + new Vector(chunkSize.X, -chunkSize.Y) / 2f,
                var _ => position
            };
            return positionWithCenterPivot;
        }
    }
}