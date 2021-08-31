using System.Collections.Generic;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace N8Engine.Tilemaps
{
    public sealed class AutoTilemap<T> where T : TilePalette, new()
    {
        public static AutoTilemap<T> Generator => new();

        private AutoTilemap() { }

        public AutoTilemap<T> PlaceChunk(Vector position, IntegerVector sizeInTiles, TilePivot pivot)
        {
            if (sizeInTiles.X <= 0 || sizeInTiles.Y <= 0) return this;

            var tilePalette = new T();
            var tileSize = new IntegerVector(tilePalette.TileSize.X * Window.RATIO_OF_HORIZONTAL_PIXELS_TO_VERTICAL_PIXELS, tilePalette.TileSize.Y);
            var chunkInformation = new ChunkInformation(position, sizeInTiles, tileSize, pivot, tilePalette);

            for (var y = 0; y < sizeInTiles.Y; y++)
                for (var x = 0; x < sizeInTiles.X; x++)
                {
                    var localPosition = new IntegerVector(x, y);
                    GameObject.Create<Tile>().Initialize(localPosition, chunkInformation);
                }
            var chunkSize = sizeInTiles * tileSize;

            var tilemapCollider = tilePalette.BaseTilemapObject;
            tilemapCollider.Transform.Position = GetColliderPositionFromPivot(position, pivot, chunkSize);
            tilemapCollider.Collider.Size = sizeInTiles * tilePalette.TileSize;
            
            return this;
        }

        private Vector GetColliderPositionFromPivot(Vector position, TilePivot pivot, Vector chunkSize)
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