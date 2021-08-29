using System.Collections.Generic;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace N8Engine.Tilemaps
{
    public sealed class AutoTilemap<T> where T : TilePalette, new()
    {
        enum TileType
        {
            Left, Right, Top, Bottom, TopRight, TopLeft, BottomRight, BottomLeft, Middle
        }

        public static AutoTilemap<T> Generator => new();

        AutoTilemap() { }

        public AutoTilemap<T> Place(Vector position, Vector sizeOfChunkInNumberOfTiles, TilePivot pivot)
        {
            if (sizeOfChunkInNumberOfTiles.X <= 0 || sizeOfChunkInNumberOfTiles.Y <= 0) return this;

            var tilePalette = new T();
            var actualTileSize = new Vector(tilePalette.TileSize.X * Window.RATIO_OF_HORIZONTAL_PIXELS_TO_VERTICAL_PIXELS, tilePalette.TileSize.Y);
            var tiles = new List<GameObject>();
            
            for (var y = 0; y < (int) sizeOfChunkInNumberOfTiles.Y; y++)
            {
                for (var x = 0; x < (int) sizeOfChunkInNumberOfTiles.X; x++)
                {
                    var tileType = GetTileType(new Vector(x, y), sizeOfChunkInNumberOfTiles);
                    var sprite = tileType switch
                    {
                        TileType.Left => tilePalette.Left,
                        TileType.Right => tilePalette.Right,
                        TileType.Top => tilePalette.Top,
                        TileType.Bottom => tilePalette.Bottom,
                        TileType.TopLeft => tilePalette.TopLeft,
                        TileType.TopRight => tilePalette.TopRight,
                        TileType.BottomLeft => tilePalette.BottomLeft,
                        TileType.BottomRight => tilePalette.BottomRight,
                        TileType.Middle => tilePalette.Middle,
                        var _ => tilePalette.Middle
                    };
                    var gameObject = GameObject.Create<EmptyGameObject>();
                    gameObject.SpriteRenderer.Sprite = sprite;
                    gameObject.Transform.Position = position + new Vector(x, y) * actualTileSize;
                    tiles.Add(gameObject);
                }
            }
            var chunkSize = sizeOfChunkInNumberOfTiles * actualTileSize;
            foreach (var tile in tiles)
                tile.Transform.Position = GetTilePositionFromPivot(pivot, tile.Transform.Position, chunkSize, actualTileSize);

            var tilemapCollider = tilePalette.BaseTilemapObject;
            tilemapCollider.Transform.Position = GetColliderPositionFromPivot(position, pivot, chunkSize);
            tilemapCollider.Collider.Size = sizeOfChunkInNumberOfTiles * tilePalette.TileSize;

            return this;
        }

        TileType GetTileType(Vector tilePosition, Vector numberOfTiles)
        {
            if (numberOfTiles.X == 1)
            {
                if (tilePosition.Y == 0) return TileType.Top;
                if (tilePosition.Y == numberOfTiles.Y - 1) return TileType.Bottom;                
                return TileType.Middle;
            }
            if (tilePosition.X == 0)
            {
                if (tilePosition.Y == 0) return TileType.TopLeft;
                if (tilePosition.Y == numberOfTiles.Y - 1) return TileType.BottomLeft;
                return TileType.Left;
            }
            if (tilePosition.X == numberOfTiles.X - 1)
            {
                if (tilePosition.Y == 0) return TileType.TopRight;
                if (tilePosition.Y == numberOfTiles.Y - 1) return TileType.BottomRight;;
                return TileType.Right;
            }
            if (tilePosition.Y == 0)
                return TileType.Top;
            if (tilePosition.Y == numberOfTiles.Y - 1)
                return TileType.Bottom;
            return TileType.Middle;
        }

        Vector GetTilePositionFromPivot(TilePivot pivot, Vector position, Vector chunkSize, Vector actualTileSize)
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

        Vector GetColliderPositionFromPivot(Vector position, TilePivot pivot, Vector chunkSize)
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